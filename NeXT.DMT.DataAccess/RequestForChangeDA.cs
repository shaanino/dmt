using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DocumentFormat.OpenXml.Packaging;
using NeXT.DMT.Entities;
using NeXT.DMT.Utilities;
using DocumentFormat.OpenXml.Wordprocessing;
using System.IO;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml;
using System.Collections.ObjectModel;

namespace NeXT.DMT.DataAccess
{
    public static class RequestForChangeDA
    {

        public static void GenerateRFC(RequestForChangeBE rfc, DeliveryInfoBE deliveryInfo, String delvEnvironment, ApplicationConfigBE appConfig)
        {
            //path to the RFC template
            String rfcTemplatePath = ConstantBE.TEMPLATEPATH + '\\' + deliveryInfo.AppQuadri + ".docx";

            String rfcNewName = String.Empty;
            String outputPath = String.Empty;
            String directoryPath = String.Empty;
            String filename = String.Empty;

            #region RFC Name Formatting And Output Path

            if (delvEnvironment == "INT")
            {
                //RFC_IPRI_ITF594_2.2.0_20120328_INT
                rfcNewName = String.Format(ConstantBE.RFCFORMATINTANDUAT, deliveryInfo.AppQuadri,
                    "ITF" + deliveryInfo.ITFNo, deliveryInfo.GetVersion(true, false), rfc.SubmittedDate.ToString("yyyyMMdd"), "INT");

                outputPath = deliveryInfo.RootDirectoryPath + '\\' + "Internals" + '\\' +
                            "ITF" + deliveryInfo.ITFNo + '\\' + "RFC\\" + rfcNewName + ".docx";
            }
            else if (delvEnvironment == "UAT")
            {
                //RFC_IPRI_ITF594_2.2.0_20120328_INT
                rfcNewName = String.Format(ConstantBE.RFCFORMATINTANDUAT, deliveryInfo.AppQuadri,
                    "ITF" + deliveryInfo.ITFNo, deliveryInfo.GetVersion(true, false), rfc.SubmittedDate.ToString("yyyyMMdd"), "UAT");

                outputPath = deliveryInfo.RootDirectoryPath + '\\' + "Internals" + '\\' +
                        "ITF" + deliveryInfo.ITFNo + '\\' + "RFC\\" + rfcNewName + ".docx";
            }
            else if (delvEnvironment == "STA")
            {
                //get the integer path from the string
                String integerPart = Regex.Match(deliveryInfo.NextExternalDirectory, @"\d+").Value;
                
                //RFC_IPRI_Liv_2_STA
                rfcNewName = String.Format(ConstantBE.RFCFORMATSTAANDPROD, deliveryInfo.AppQuadri,
                    "Liv", integerPart, "STA");

                outputPath = deliveryInfo.RootDirectoryPath + '\\' + "Externals" + '\\' +
                    deliveryInfo.NextExternalDirectory + '\\' + "DOCUMENTATION\\" + rfcNewName + ".docx";
            }
            else
            {
                //get the integer path from the string
                String integerPart = Regex.Match(deliveryInfo.NextExternalDirectory, @"\d+").Value;

                //RFC_IPRI_Liv_2_PROD
                rfcNewName = String.Format(ConstantBE.RFCFORMATSTAANDPROD, deliveryInfo.AppQuadri,
                    "Liv", integerPart, "PROD");

                outputPath = deliveryInfo.RootDirectoryPath + '\\' + "Externals" + '\\' +
                    deliveryInfo.NextExternalDirectory + '\\' + "DOCUMENTATION\\" + rfcNewName + ".docx";
            }

            #endregion

            DirectoryManagementDA.CopyFileAndRename(rfcTemplatePath, outputPath);

            //open the copied file in its new location for modification
            using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(outputPath, true))
            {
                //get the document body
                MainDocumentPart mainDocPart = wordDoc.MainDocumentPart;

                #region Change Identification

                #region Requestor Information

                //get only the change identification body from the document
                SdtBlock rfcBody = OpenXmlHelpers.GetContentControl(mainDocPart, ConstantBE.CHANGEIDENTIFICATIONBODY);

                //Get the table in the change identification tag
                Table rfcBodyTable = rfcBody.Descendants<Table>().Single();

                //Entity
                rfcBodyTable.Elements<TableRow>().ElementAt(3).Elements<SdtCell>()
                    .ElementAt(0).Descendants<Text>().Single().Text = rfc.Entity;

                //Submitted by
                rfcBodyTable.Elements<TableRow>().ElementAt(3).Elements<SdtCell>()
                    .ElementAt(1).Descendants<Text>().Single().Text = rfc.SubmittedBy;

                //Sumitted date
                rfcBodyTable.Elements<TableRow>().ElementAt(3).Elements<SdtCell>()
                    .ElementAt(2).Descendants<Text>().Single().Text = rfc.SubmittedDate.ToString("dd/MM/yy");

                #endregion

                #region Qualification Of Request Checkbox

                Paragraph qualOfRequestPara = new Paragraph();

                //based on the qualification of request we get the corresponding
                //element
                switch (rfc.QualificationOfRequest)
                {
                    case "Major":
                        qualOfRequestPara = rfcBodyTable.Elements<TableRow>().ElementAt(5)
                            .Elements<TableCell>().ElementAt(0).Elements<Paragraph>().FirstOrDefault();
                        break;

                    case "Medium":
                        qualOfRequestPara = rfcBodyTable.Elements<TableRow>().ElementAt(5)
                            .Elements<TableCell>().ElementAt(1).Elements<Paragraph>().FirstOrDefault();
                        break;

                    default:
                        qualOfRequestPara = rfcBodyTable.Elements<TableRow>().ElementAt(5)
                            .Elements<TableCell>().ElementAt(2).Elements<Paragraph>().FirstOrDefault();
                        break;
                }

                CheckBox qualOfRequestCB = qualOfRequestPara.Descendants<CheckBox>().FirstOrDefault();

                if (qualOfRequestCB != null)
                {
                    //get the checkbox
                    DefaultCheckBoxFormFieldState dcb = qualOfRequestCB.Elements<DefaultCheckBoxFormFieldState>()
                    .FirstOrDefault();
                    
                    //set it to true (Checked)
                    dcb.Val = true;
                }

                #endregion

                #region Priority Checkbox

                Paragraph priorityPara = new Paragraph();

                //based on the priority we get the corresponding
                //element
                switch (rfc.Priority)
                {
                    case "1":
                        priorityPara = rfcBodyTable.Elements<TableRow>().ElementAt(7)
                            .Elements<TableCell>().ElementAt(0).Elements<Paragraph>().FirstOrDefault();
                        break;

                    case "2":
                        priorityPara = rfcBodyTable.Elements<TableRow>().ElementAt(7)
                            .Elements<TableCell>().ElementAt(1).Elements<Paragraph>().FirstOrDefault();
                        break;

                    default:
                        priorityPara = rfcBodyTable.Elements<TableRow>().ElementAt(7)
                            .Elements<TableCell>().ElementAt(2).Elements<Paragraph>().FirstOrDefault();
                        break;
                }

                CheckBox priorityCB = priorityPara.Descendants<CheckBox>().FirstOrDefault();

                if (priorityCB != null)
                {
                    //get the checkbox
                    DefaultCheckBoxFormFieldState dcb = priorityCB.Elements<DefaultCheckBoxFormFieldState>()
                    .FirstOrDefault();

                    //set it to true (Checked)
                    dcb.Val = true;
                }

                #endregion

                #region Regulated Environment Checkbox

                Paragraph regulatedEnvPara = new Paragraph();

                //based on the regulated environment we get the corresponding
                //element
                switch (rfc.RegulatedEnvironment)
                {
                    case "GxPImpact":
                        regulatedEnvPara = rfcBodyTable.Elements<TableRow>().ElementAt(9)
                            .Elements<TableCell>().ElementAt(0).Elements<Paragraph>().FirstOrDefault();
                        break;

                    case "SOAImpact":
                        regulatedEnvPara = rfcBodyTable.Elements<TableRow>().ElementAt(9)
                            .Elements<TableCell>().ElementAt(1).Elements<Paragraph>().FirstOrDefault();
                        break;

                    default:
                        regulatedEnvPara = rfcBodyTable.Elements<TableRow>().ElementAt(9)
                            .Elements<TableCell>().ElementAt(2).Elements<Paragraph>().FirstOrDefault();
                        break;
                }

                CheckBox regulatedEnvCB = regulatedEnvPara.Descendants<CheckBox>().FirstOrDefault();

                if (regulatedEnvCB != null)
                {
                    //get the checkbox
                    DefaultCheckBoxFormFieldState dcb = regulatedEnvCB.Elements<DefaultCheckBoxFormFieldState>()
                    .FirstOrDefault();

                    //set it to true (Checked)
                    dcb.Val = true;
                }

                #endregion

                #region Expected Finish 
                
                //Expected finish date
                rfcBodyTable.Elements<TableRow>().ElementAt(12).Elements<SdtCell>()
                    .ElementAt(0).Descendants<Text>().Single().Text = rfc.ExpectedDate;


                //Expected finish hour
                rfcBodyTable.Elements<TableRow>().ElementAt(12).Elements<SdtCell>()
                    .ElementAt(1).Descendants<Text>().Single().Text = rfc.ExpectedStartTime;

                //Expecte finish duration
                rfcBodyTable.Elements<TableRow>().ElementAt(12).Elements<SdtCell>()
                    .ElementAt(2).Descendants<Text>().Single().Text = rfc.ExpectedDuration;
                
                #endregion

                #region Change Region

                //we get the change reason table cell
                TableCell changeResonTableCell = rfcBodyTable.Elements<TableRow>().ElementAt(14)
                       .Elements<TableCell>().ElementAt(0);

                //Change reason
                changeResonTableCell.Elements<SdtBlock>().Where
                        (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.CHANGEREASON).Single()
                        .Descendants<Text>().Single().Text = rfc.ChangeReason;

                //Environment
                changeResonTableCell.Elements<Paragraph>().ElementAt(3)
                    .Elements<SdtRun>().Where
                        (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.ENVIRONMENT).Single()
                        .Descendants<Text>().Single().Text = rfc.Environment;

                //ServerInformation

                /*changeResonTableCell.Elements<SdtBlock>().Where
                    (r => r.SdtProperties.GetFirstChild<Tag>().Val == ContantsBE.SERVERINFORMATION).Single()
                    .Descendants<Text>().Single().Text = rfc.ServerInformation;
                */

                String[] splitedServerInformation = rfc.ServerInformation.Split('|');

                foreach (String item in splitedServerInformation)
                {
                    //only if have item
                    if (!String.IsNullOrEmpty(item))
                    {
                        Paragraph newPara = new Paragraph();

                        Run newRun = new Run();

                        //add a new line and the append the item
                        //newRun.AppendChild(new Break());
                        newRun.AppendChild(new Text(item));

                        newPara.Append(newRun);

                        changeResonTableCell.Elements<SdtBlock>().Where
                        (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.SERVERINFORMATION).Single().Append(newPara);

                    }
                }

                //Impact on service
                changeResonTableCell.Elements<SdtBlock>().Where
                    (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.IMPACTONSERVICE).Single()
                    .Descendants<Text>().Single().Text = rfc.ImpactOnService;

                #endregion

                #endregion

                #region Backup And Maintenance

                SdtBlock descReqBackBody = OpenXmlHelpers.GetContentControl(mainDocPart, ConstantBE.DESCRIPTIONREQUESTBODY);

                //Get the table in the description body tag
                Table descReqBackTable = descReqBackBody.Descendants<Table>().Single();

                //if we are not delivering WEB we remove the backup directive
                if (!deliveryInfo.isDelvWEB)
                {
                    //check if the tag exists before removing else 
                    //it will cause an exception
                    //and we don't want it to crash here!!
                    if (descReqBackTable.Elements<TableRow>().ElementAt(1)
                        .Elements<TableCell>().ElementAt(0)
                        .Elements<SdtBlock>().Where
                    (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.WEBBACKUPDIRECTIVE).Any())
                    {
                        descReqBackTable.Elements<TableRow>().ElementAt(1)
                            .Elements<TableCell>().ElementAt(0)
                            .Elements<SdtBlock>().Where
                        (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.WEBBACKUPDIRECTIVE).Single().Remove();
                    }
                }

                //if we are not delivering WS we remove the backup directive
                if (!deliveryInfo.isDelvWS)
                {
                    //check if the tag exists before removing else 
                    //it will cause an exception
                    //and we don't want it to crash here!!
                    if (descReqBackTable.Elements<TableRow>().ElementAt(1)
                        .Elements<TableCell>().ElementAt(0)
                        .Elements<SdtBlock>().Where
                    (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.WSBACKUPDIRECTIVE).Any())
                    {
                        descReqBackTable.Elements<TableRow>().ElementAt(1)
                            .Elements<TableCell>().ElementAt(0)
                            .Elements<SdtBlock>().Where
                        (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.WSBACKUPDIRECTIVE).Single().Remove();
                    }
                }

                //if we are not delivering DB we remove the backup directive
                if (!deliveryInfo.isDelvDB)
                {
                    //check if the tag exists before removing else 
                    //it will cause an exception
                    //and we don't want it to crash here!!
                    if (descReqBackTable.Elements<TableRow>().ElementAt(1)
                        .Elements<TableCell>().ElementAt(0)
                        .Elements<SdtBlock>().Where
                    (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.DATABASEBACKUPDIRECTIVE).Any())
                    {
                        descReqBackTable.Elements<TableRow>().ElementAt(1)
                            .Elements<TableCell>().ElementAt(0)
                            .Elements<SdtBlock>().Where
                        (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.DATABASEBACKUPDIRECTIVE).Single().Remove();
                    }
                }
                else
                {
#warning this logic should be modified
                    
                    descReqBackTable.Elements<TableRow>().ElementAt(1)
                            .Elements<TableCell>().ElementAt(0)
                            .Elements<SdtBlock>().Where
                        (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.DATABASEBACKUPDIRECTIVE).Single()
                        .Descendants<Text>().ElementAt(1).Text = "Please do a backup of the schema " + appConfig.DatabaseServers[0].InstanceName 
                        + " before proceeding.";
                    
 
                }

                //if we are not delivering K2 we remove the backup directive
                if (!deliveryInfo.isDelvK2PROCESS)
                {
                    //check if the tag exists before removing else 
                    //it will cause an exception
                    //and we don't want it to crash here!!
                    if (descReqBackTable.Elements<TableRow>().ElementAt(1)
                        .Elements<TableCell>().ElementAt(0)
                        .Elements<SdtBlock>().Where
                    (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.K2BACKUPDIRECTIVE).Any())
                    {
                        descReqBackTable.Elements<TableRow>().ElementAt(1)
                            .Elements<TableCell>().ElementAt(0)
                            .Elements<SdtBlock>().Where
                        (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.K2BACKUPDIRECTIVE).Single().Remove();
                    }
                }

                //if we are not delivering REPORT we remove the backup directive
                if (!deliveryInfo.isDelvREPORT)
                {
                    //check if the tag exists before removing else 
                    //it will cause an exception
                    //and we don't want it to crash here!!
                    if (descReqBackTable.Elements<TableRow>().ElementAt(1)
                        .Elements<TableCell>().ElementAt(0)
                        .Elements<SdtBlock>().Where
                    (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.REPORTBACKUPDIRECTIVE).Any())
                    {
                        descReqBackTable.Elements<TableRow>().ElementAt(1)
                            .Elements<TableCell>().ElementAt(0)
                            .Elements<SdtBlock>().Where
                        (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.REPORTBACKUPDIRECTIVE).Single().Remove();
                    }
                }

                //if we are not delivering BATCH we remove the backup directive
                if (!deliveryInfo.isDelvBATCH)
                {
                    //check if the tag exists before removing else 
                    //it will cause an exception
                    //and we don't want it to crash here!!
                    if (descReqBackTable.Elements<TableRow>().ElementAt(1)
                        .Elements<TableCell>().ElementAt(0)
                        .Elements<SdtBlock>().Where
                    (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.BATCHBACKUPDIRECTIVE).Any())
                    {
                        descReqBackTable.Elements<TableRow>().ElementAt(1)
                            .Elements<TableCell>().ElementAt(0)
                            .Elements<SdtBlock>().Where
                        (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.BATCHBACKUPDIRECTIVE).Single().Remove();
                    }
                }

                #endregion

                #region Deployment

                SdtBlock descReqDeployBody = OpenXmlHelpers.GetContentControl(mainDocPart, ConstantBE.SETUPDESCRIPTIONBODY);

                //Get the table in the description body tag
                Table descReqDeployTable = descReqDeployBody.Descendants<Table>().Single();

                #region WEB

                //check if we are delivering WEB else remove the tag
                if (deliveryInfo.isDelvWEB)
                {
                    //setup directory path
                    directoryPath = DirectoryManagementDA.GetDirectoryPath(deliveryInfo, delvEnvironment, "WEB", "SETUP");

                    //set the path to the setup file
                    descReqDeployTable.Elements<TableRow>().ElementAt(1)
                        .Elements<TableCell>().ElementAt(0)
                        .Elements<SdtBlock>().Where
                    (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.WEBDEPLOYMENT).FirstOrDefault()
                    .Elements<SdtContentBlock>().ElementAt(0)
                        .Elements<SdtBlock>().Where
                    (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.PATHTOFILE).FirstOrDefault()
                    .SdtContentBlock.Append(OpenXmlHelpers.GenerateParagraphWithHyperLink(mainDocPart, directoryPath));

                    filename = McopDA.GetMcopName(deliveryInfo.AppQuadri, deliveryInfo.GetVersion(true, false), "WEB", "SETUP");


                    descReqDeployTable.Elements<TableRow>().ElementAt(1)
                        .Elements<TableCell>().ElementAt(0)
                        .Elements<SdtBlock>().Where
                    (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.WEBDEPLOYMENT).FirstOrDefault()
                    .Elements<SdtContentBlock>().ElementAt(0)
                        .Elements<SdtBlock>().Where
                    (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.FILENAME).FirstOrDefault()
                    .Descendants<Text>().Single().Text = filename;

                    //logs directory path
                    directoryPath = DirectoryManagementDA.GetDirectoryPath(deliveryInfo, delvEnvironment, "WEB", "LOGS");


                    //append the log path
                    descReqDeployTable.Elements<TableRow>().ElementAt(1)
                        .Elements<TableCell>().ElementAt(0)
                        .Elements<SdtBlock>().Where
                    (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.WEBDEPLOYMENT).FirstOrDefault()
                    .Elements<SdtContentBlock>().ElementAt(0)
                        .Elements<SdtBlock>().Where
                    (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.PATHTOSTORERESULT).FirstOrDefault()
                    .SdtContentBlock.Append(OpenXmlHelpers.GenerateParagraphWithHyperLink(mainDocPart, directoryPath));

                }
                else
                {
                    //check if it exist in the document before removing
                    if (descReqDeployTable.Elements<TableRow>().ElementAt(1)
                        .Elements<TableCell>().ElementAt(0)
                        .Elements<SdtBlock>().Where
                    (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.WEBDEPLOYMENT).Any())
                    {
                        descReqDeployTable.Elements<TableRow>().ElementAt(1)
                            .Elements<TableCell>().ElementAt(0)
                            .Elements<SdtBlock>().Where
                        (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.WEBDEPLOYMENT).Single().Remove();
                    }
                }

                #endregion

                #region WS

                //check if we are delivering WS else remove the tag
                if (deliveryInfo.isDelvWS)
                {
                    //setup directory path
                    directoryPath = DirectoryManagementDA.GetDirectoryPath(deliveryInfo, delvEnvironment, "WS", "SETUP");

                    //set the path to the setup file
                    descReqDeployTable.Elements<TableRow>().ElementAt(1)
                        .Elements<TableCell>().ElementAt(0)
                        .Elements<SdtBlock>().Where
                    (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.WSDEPLOYMENT).FirstOrDefault()
                    .Elements<SdtContentBlock>().ElementAt(0)
                        .Elements<SdtBlock>().Where
                    (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.PATHTOFILE).FirstOrDefault()
                    .SdtContentBlock.Append(OpenXmlHelpers.GenerateParagraphWithHyperLink(mainDocPart, directoryPath));

                    filename = McopDA.GetMcopName(deliveryInfo.AppQuadri, deliveryInfo.GetVersion(true, false), "WS", "SETUP");


                    descReqDeployTable.Elements<TableRow>().ElementAt(1)
                        .Elements<TableCell>().ElementAt(0)
                        .Elements<SdtBlock>().Where
                    (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.WSDEPLOYMENT).FirstOrDefault()
                    .Elements<SdtContentBlock>().ElementAt(0)
                        .Elements<SdtBlock>().Where
                    (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.FILENAME).FirstOrDefault()
                    .Descendants<Text>().Single().Text = filename;

                    //logs directory path
                    directoryPath = DirectoryManagementDA.GetDirectoryPath(deliveryInfo, delvEnvironment, "WS", "LOGS");


                    //append the log path
                    descReqDeployTable.Elements<TableRow>().ElementAt(1)
                        .Elements<TableCell>().ElementAt(0)
                        .Elements<SdtBlock>().Where
                    (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.WSDEPLOYMENT).FirstOrDefault()
                    .Elements<SdtContentBlock>().ElementAt(0)
                        .Elements<SdtBlock>().Where
                    (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.PATHTOSTORERESULT).FirstOrDefault()
                    .SdtContentBlock.Append(OpenXmlHelpers.GenerateParagraphWithHyperLink(mainDocPart, directoryPath));
                }
                else
                {
                    //check if it exist in the document before removing
                    if (descReqDeployTable.Elements<TableRow>().ElementAt(1)
                        .Elements<TableCell>().ElementAt(0)
                        .Elements<SdtBlock>().Where
                    (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.WSDEPLOYMENT).Any())
                    {
                        descReqDeployTable.Elements<TableRow>().ElementAt(1)
                            .Elements<TableCell>().ElementAt(0)
                            .Elements<SdtBlock>().Where
                        (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.WSDEPLOYMENT).Single().Remove();
                    }
                }


                #endregion

                #region DB

                //check if we are delivering DB else remove the tag
                if (deliveryInfo.isDelvDB)
                {
                    //setup directory path
                    directoryPath = DirectoryManagementDA.GetDirectoryPath(deliveryInfo, delvEnvironment, "DB", "SETUP");

                    //set the path to the setup file
                    descReqDeployTable.Elements<TableRow>().ElementAt(1)
                        .Elements<TableCell>().ElementAt(0)
                        .Elements<SdtBlock>().Where
                    (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.DATABASEDEPLOYMENT).FirstOrDefault()
                    .Elements<SdtContentBlock>().ElementAt(0)
                        .Elements<SdtBlock>().Where
                    (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.PATHTOFILE).FirstOrDefault()
                    .SdtContentBlock.Append(OpenXmlHelpers.GenerateParagraphWithHyperLink(mainDocPart, directoryPath));


                    descReqDeployTable.Elements<TableRow>().ElementAt(1)
                        .Elements<TableCell>().ElementAt(0)
                        .Elements<SdtBlock>().Where
                    (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.DATABASEDEPLOYMENT).FirstOrDefault()
                    .Elements<SdtContentBlock>().ElementAt(0)
                        .Elements<SdtBlock>().Where
                    (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.FILENAME).FirstOrDefault()
                    .Descendants<Text>().Single().Text = "Enter your script name";

                    //logs directory path
                    directoryPath = DirectoryManagementDA.GetDirectoryPath(deliveryInfo, delvEnvironment, "DB", "LOGS");


                    //append the log path
                    descReqDeployTable.Elements<TableRow>().ElementAt(1)
                        .Elements<TableCell>().ElementAt(0)
                        .Elements<SdtBlock>().Where
                    (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.DATABASEDEPLOYMENT).FirstOrDefault()
                    .Elements<SdtContentBlock>().ElementAt(0)
                        .Elements<SdtBlock>().Where
                    (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.PATHTOSTORERESULT).FirstOrDefault()
                    .SdtContentBlock.Append(OpenXmlHelpers.GenerateParagraphWithHyperLink(mainDocPart, directoryPath));
                }
                else
                {
                    //check if it exist in the document before removing
                    if (descReqDeployTable.Elements<TableRow>().ElementAt(1)
                        .Elements<TableCell>().ElementAt(0)
                        .Elements<SdtBlock>().Where
                    (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.DATABASEDEPLOYMENT).Any())
                    {
                        descReqDeployTable.Elements<TableRow>().ElementAt(1)
                            .Elements<TableCell>().ElementAt(0)
                            .Elements<SdtBlock>().Where
                        (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.DATABASEDEPLOYMENT).Single().Remove();
                    }
                }

                #endregion

                #region K2PROCESS

                //check if we are delivering K2 else remove the tag
                if (deliveryInfo.isDelvK2PROCESS)
                {

                }
                else
                {
                    //check if it exist in the document before removing
                    if (descReqDeployTable.Elements<TableRow>().ElementAt(1)
                        .Elements<TableCell>().ElementAt(0)
                        .Elements<SdtBlock>().Where
                    (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.K2DEPLOYMENT).Any())
                    {
                        descReqDeployTable.Elements<TableRow>().ElementAt(1)
                            .Elements<TableCell>().ElementAt(0)
                            .Elements<SdtBlock>().Where
                        (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.K2DEPLOYMENT).Single().Remove();
                    }
                }
                
                #endregion

                #region REPORT 

                //check if we are delivering REPORT else remove the tag
                if (deliveryInfo.isDelvREPORT)
                {

                }
                else
                {
                    //check if it exist in the document before removing
                    if (descReqDeployTable.Elements<TableRow>().ElementAt(1)
                        .Elements<TableCell>().ElementAt(0)
                        .Elements<SdtBlock>().Where
                    (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.REPORTDEPLOYMENT).Any())
                    {
                        descReqDeployTable.Elements<TableRow>().ElementAt(1)
                            .Elements<TableCell>().ElementAt(0)
                            .Elements<SdtBlock>().Where
                        (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.REPORTDEPLOYMENT).Single().Remove();
                    }
                }

                #endregion

                #region BATCH

                //check if we are delivering BATCH else remove the tag
                if (deliveryInfo.isDelvBATCH)
                {
                    //setup directory path
                    directoryPath = DirectoryManagementDA.GetDirectoryPath(deliveryInfo, delvEnvironment, "BATCH", "SETUP");

                    //set the path to the setup file
                    descReqDeployTable.Elements<TableRow>().ElementAt(1)
                        .Elements<TableCell>().ElementAt(0)
                        .Elements<SdtBlock>().Where
                    (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.BATCHDEPLOYMENT).FirstOrDefault()
                    .Elements<SdtContentBlock>().ElementAt(0)
                        .Elements<SdtBlock>().Where
                    (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.PATHTOFILE).FirstOrDefault()
                    .SdtContentBlock.Append(OpenXmlHelpers.GenerateParagraphWithHyperLink(mainDocPart, directoryPath));

                    filename = McopDA.GetMcopName(deliveryInfo.AppQuadri, deliveryInfo.GetVersion(true, false), "BATCH", "SETUP");


                    descReqDeployTable.Elements<TableRow>().ElementAt(1)
                        .Elements<TableCell>().ElementAt(0)
                        .Elements<SdtBlock>().Where
                    (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.BATCHDEPLOYMENT).FirstOrDefault()
                    .Elements<SdtContentBlock>().ElementAt(0)
                        .Elements<SdtBlock>().Where
                    (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.FILENAME).FirstOrDefault()
                    .Descendants<Text>().Single().Text = filename;

                    //logs directory path
                    directoryPath = DirectoryManagementDA.GetDirectoryPath(deliveryInfo, delvEnvironment, "BATCH", "LOGS");


                    //append the log path
                    descReqDeployTable.Elements<TableRow>().ElementAt(1)
                        .Elements<TableCell>().ElementAt(0)
                        .Elements<SdtBlock>().Where
                    (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.BATCHDEPLOYMENT).FirstOrDefault()
                    .Elements<SdtContentBlock>().ElementAt(0)
                        .Elements<SdtBlock>().Where
                    (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.PATHTOSTORERESULT).FirstOrDefault()
                    .SdtContentBlock.Append(OpenXmlHelpers.GenerateParagraphWithHyperLink(mainDocPart, directoryPath));
                }
                else
                {
                    //check if it exist in the document before removing
                    if (descReqDeployTable.Elements<TableRow>().ElementAt(1)
                        .Elements<TableCell>().ElementAt(0)
                        .Elements<SdtBlock>().Where
                    (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.BATCHDEPLOYMENT).Any())
                    {
                        descReqDeployTable.Elements<TableRow>().ElementAt(1)
                            .Elements<TableCell>().ElementAt(0)
                            .Elements<SdtBlock>().Where
                        (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.BATCHDEPLOYMENT).Single().Remove();
                    }
                }

                #endregion

                #region WEB.CONFIG

                //check if we are modifying the web.config else remove the tag
                if (deliveryInfo.isModifWebConfig)
                {

                    foreach (String item in ApplicationDA.GetApplicationWebConfig(appConfig))
                    {
                        descReqDeployTable.Elements<TableRow>().ElementAt(1)
                            .Elements<TableCell>().ElementAt(0)
                            .Elements<SdtBlock>().Where
                        (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.WEBCONFIGMODIF).FirstOrDefault()
                        .Elements<SdtContentBlock>().ElementAt(0)
                            .Elements<SdtBlock>().Where
                        (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.PATHTOFILE).FirstOrDefault()
                        .SdtContentBlock.Append(OpenXmlHelpers.GenerateParagraphWithHyperLink(mainDocPart, item));
                    }

                    descReqDeployTable.Elements<TableRow>().ElementAt(1)
                        .Elements<TableCell>().ElementAt(0)
                        .Elements<SdtBlock>().Where
                    (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.WEBCONFIGMODIF).FirstOrDefault()
                    .Elements<SdtContentBlock>().ElementAt(0)
                    .Elements<Paragraph>().ElementAt(5).Descendants<SdtRun>().FirstOrDefault()
                    .Descendants<Text>().Single().Text = deliveryInfo.GetVersion(true, true);

                    descReqDeployTable.Elements<TableRow>().ElementAt(1)
                        .Elements<TableCell>().ElementAt(0)
                        .Elements<SdtBlock>().Where
                    (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.WEBCONFIGMODIF).FirstOrDefault()
                    .Elements<SdtContentBlock>().ElementAt(0)
                    .Elements<Paragraph>().ElementAt(9).Descendants<SdtRun>().ElementAt(0)
                    .Descendants<Text>().Single().Text = ApplicationDA.GetWebConfigFullEnvironmentCode(delvEnvironment);

                    descReqDeployTable.Elements<TableRow>().ElementAt(1)
                        .Elements<TableCell>().ElementAt(0)
                        .Elements<SdtBlock>().Where
                    (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.WEBCONFIGMODIF).FirstOrDefault()
                    .Elements<SdtContentBlock>().ElementAt(0)
                    .Elements<Paragraph>().ElementAt(9).Descendants<SdtRun>().ElementAt(1)
                    .Descendants<Text>().Single().Text = deliveryInfo.GetVersion(true, true);

                }
                else
                {
                    //check if it exist in the document before removing
                    if (descReqDeployTable.Elements<TableRow>().ElementAt(1)
                        .Elements<TableCell>().ElementAt(0)
                        .Elements<SdtBlock>().Where
                    (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.WEBCONFIGMODIF).Any())
                    {
                        descReqDeployTable.Elements<TableRow>().ElementAt(1)
                            .Elements<TableCell>().ElementAt(0)
                            .Elements<SdtBlock>().Where
                        (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.WEBCONFIGMODIF).Single().Remove();
                    }
                }

                #endregion
                
                #endregion

                #region RollBack

                SdtBlock rollBackBody = OpenXmlHelpers.GetContentControl(mainDocPart, ConstantBE.TESTINGROLLBACKBODY);

                //Get the table in the rollback body tag
                Table rollBackTable = rollBackBody.Descendants<Table>().Single();

                #region Required Testing

                rollBackTable.Elements<TableRow>().ElementAt(1)
                        .Elements<TableCell>().ElementAt(0)
                        .Elements<SdtBlock>().Where
                    (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.REQUIREDTESTINGURL).FirstOrDefault()
                    .Descendants<Paragraph>().FirstOrDefault().Descendants<Text>().Single().Text = appConfig.AppTestUrl;

                #endregion

                #region WEB

                //check if we are delivering WEB else remove the tag
                if (deliveryInfo.isDelvWEB)
                {
                    //setup directory path
                    directoryPath = DirectoryManagementDA.GetDirectoryPath(deliveryInfo, delvEnvironment, "WEB", "ROLLBACK");

                    //set the path to the setup file
                    rollBackTable.Elements<TableRow>().ElementAt(3)
                        .Elements<TableCell>().ElementAt(0)
                        .Elements<SdtBlock>().Where
                    (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.WEBROLLBACK).FirstOrDefault()
                    .Elements<SdtContentBlock>().ElementAt(0)
                        .Elements<SdtBlock>().Where
                    (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.PATHTOFILE).FirstOrDefault()
                    .SdtContentBlock.Append(OpenXmlHelpers.GenerateParagraphWithHyperLink(mainDocPart, directoryPath));

                    filename = McopDA.GetMcopName(deliveryInfo.AppQuadri, deliveryInfo.GetVersion(false, false), "WEB", "ROLLBACK");


                    rollBackTable.Elements<TableRow>().ElementAt(3)
                        .Elements<TableCell>().ElementAt(0)
                        .Elements<SdtBlock>().Where
                    (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.WEBROLLBACK).FirstOrDefault()
                    .Elements<SdtContentBlock>().ElementAt(0)
                        .Elements<SdtBlock>().Where
                    (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.FILENAME).FirstOrDefault()
                    .Descendants<Text>().Single().Text = filename;

                    //logs directory path
                    directoryPath = DirectoryManagementDA.GetDirectoryPath(deliveryInfo, delvEnvironment, "WEB", "LOGS");


                    //append the log path
                    rollBackTable.Elements<TableRow>().ElementAt(3)
                        .Elements<TableCell>().ElementAt(0)
                        .Elements<SdtBlock>().Where
                    (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.WEBROLLBACK).FirstOrDefault()
                    .Elements<SdtContentBlock>().ElementAt(0)
                        .Elements<SdtBlock>().Where
                    (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.PATHTOSTORERESULT).FirstOrDefault()
                    .SdtContentBlock.Append(OpenXmlHelpers.GenerateParagraphWithHyperLink(mainDocPart, directoryPath));

                }
                else
                {
                    //check if it exist in the document before removing
                    if (rollBackTable.Elements<TableRow>().ElementAt(3)
                        .Elements<TableCell>().ElementAt(0)
                        .Elements<SdtBlock>().Where
                    (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.WEBROLLBACK).Any())
                    {
                        rollBackTable.Elements<TableRow>().ElementAt(3)
                            .Elements<TableCell>().ElementAt(0)
                            .Elements<SdtBlock>().Where
                        (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.WEBROLLBACK).Single().Remove();
                    }
                }

                #endregion

                #region WS

                //check if we are delivering WS else remove the tag
                if (deliveryInfo.isDelvWS)
                {
                    //setup directory path
                    directoryPath = DirectoryManagementDA.GetDirectoryPath(deliveryInfo, delvEnvironment, "WS", "ROLLBACK");

                    //set the path to the setup file
                    rollBackTable.Elements<TableRow>().ElementAt(3)
                        .Elements<TableCell>().ElementAt(0)
                        .Elements<SdtBlock>().Where
                    (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.WSROLLBACK).FirstOrDefault()
                    .Elements<SdtContentBlock>().ElementAt(0)
                        .Elements<SdtBlock>().Where
                    (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.PATHTOFILE).FirstOrDefault()
                    .SdtContentBlock.Append(OpenXmlHelpers.GenerateParagraphWithHyperLink(mainDocPart, directoryPath));

                    filename = McopDA.GetMcopName(deliveryInfo.AppQuadri, deliveryInfo.GetVersion(false, false), "WS", "ROLLBACK");


                    rollBackTable.Elements<TableRow>().ElementAt(3)
                        .Elements<TableCell>().ElementAt(0)
                        .Elements<SdtBlock>().Where
                    (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.WSROLLBACK).FirstOrDefault()
                    .Elements<SdtContentBlock>().ElementAt(0)
                        .Elements<SdtBlock>().Where
                    (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.FILENAME).FirstOrDefault()
                    .Descendants<Text>().Single().Text = filename;

                    //logs directory path
                    directoryPath = DirectoryManagementDA.GetDirectoryPath(deliveryInfo, delvEnvironment, "WS", "LOGS");


                    //append the log path
                    rollBackTable.Elements<TableRow>().ElementAt(3)
                        .Elements<TableCell>().ElementAt(0)
                        .Elements<SdtBlock>().Where
                    (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.WSROLLBACK).FirstOrDefault()
                    .Elements<SdtContentBlock>().ElementAt(0)
                        .Elements<SdtBlock>().Where
                    (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.PATHTOSTORERESULT).FirstOrDefault()
                    .SdtContentBlock.Append(OpenXmlHelpers.GenerateParagraphWithHyperLink(mainDocPart, directoryPath));
                }
                else
                {
                    //check if it exist in the document before removing
                    if (rollBackTable.Elements<TableRow>().ElementAt(3)
                        .Elements<TableCell>().ElementAt(0)
                        .Elements<SdtBlock>().Where
                    (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.WSROLLBACK).Any())
                    {
                        rollBackTable.Elements<TableRow>().ElementAt(3)
                            .Elements<TableCell>().ElementAt(0)
                            .Elements<SdtBlock>().Where
                        (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.WSROLLBACK).Single().Remove();
                    }
                }


                #endregion

                #region DB

                //check if we are delivering DB else remove the tag
                if (deliveryInfo.isDelvDB)
                {
                    //we can add some logic here also :)
                }
                else
                {
                    //check if it exist in the document before removing
                    if (rollBackTable.Elements<TableRow>().ElementAt(3)
                        .Elements<TableCell>().ElementAt(0)
                        .Elements<SdtBlock>().Where
                    (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.DATABASEROLLBACK).Any())
                    {
                        rollBackTable.Elements<TableRow>().ElementAt(3)
                            .Elements<TableCell>().ElementAt(0)
                            .Elements<SdtBlock>().Where
                        (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.DATABASEROLLBACK).Single().Remove();
                    }
                }

                #endregion

                #region REPORT

                //check if we are delivering REPORT else remove the tag
                if (deliveryInfo.isDelvREPORT)
                {

                }
                else
                {
                    //check if it exist in the document before removing
                    if (rollBackTable.Elements<TableRow>().ElementAt(3)
                        .Elements<TableCell>().ElementAt(0)
                        .Elements<SdtBlock>().Where
                    (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.REPORTROLLBACK).Any())
                    {
                        rollBackTable.Elements<TableRow>().ElementAt(3)
                            .Elements<TableCell>().ElementAt(0)
                            .Elements<SdtBlock>().Where
                        (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.REPORTROLLBACK).Single().Remove();
                    }
                }

                #endregion

                #region BATCH

                //check if we are delivering BATCH else remove the tag
                if (deliveryInfo.isDelvBATCH)
                {
                    //setup directory path
                    directoryPath = DirectoryManagementDA.GetDirectoryPath(deliveryInfo, delvEnvironment, "BATCH", "ROLLBACK");

                    //set the path to the setup file
                    rollBackTable.Elements<TableRow>().ElementAt(3)
                        .Elements<TableCell>().ElementAt(0)
                        .Elements<SdtBlock>().Where
                    (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.BATCHROLLBACK).FirstOrDefault()
                    .Elements<SdtContentBlock>().ElementAt(0)
                        .Elements<SdtBlock>().Where
                    (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.PATHTOFILE).FirstOrDefault()
                    .SdtContentBlock.Append(OpenXmlHelpers.GenerateParagraphWithHyperLink(mainDocPart, directoryPath));

                    filename = McopDA.GetMcopName(deliveryInfo.AppQuadri, deliveryInfo.GetVersion(false, false), "BATCH", "ROLLBACK");


                    rollBackTable.Elements<TableRow>().ElementAt(3)
                        .Elements<TableCell>().ElementAt(0)
                        .Elements<SdtBlock>().Where
                    (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.BATCHROLLBACK).FirstOrDefault()
                    .Elements<SdtContentBlock>().ElementAt(0)
                        .Elements<SdtBlock>().Where
                    (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.FILENAME).FirstOrDefault()
                    .Descendants<Text>().Single().Text = filename;

                    //logs directory path
                    directoryPath = DirectoryManagementDA.GetDirectoryPath(deliveryInfo, delvEnvironment, "BATCH", "LOGS");


                    //append the log path
                    rollBackTable.Elements<TableRow>().ElementAt(3)
                        .Elements<TableCell>().ElementAt(0)
                        .Elements<SdtBlock>().Where
                    (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.BATCHROLLBACK).FirstOrDefault()
                    .Elements<SdtContentBlock>().ElementAt(0)
                        .Elements<SdtBlock>().Where
                    (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.PATHTOSTORERESULT).FirstOrDefault()
                    .SdtContentBlock.Append(OpenXmlHelpers.GenerateParagraphWithHyperLink(mainDocPart, directoryPath));

                }
                else
                {
                    //check if it exist in the document before removing
                    if (rollBackTable.Elements<TableRow>().ElementAt(3)
                        .Elements<TableCell>().ElementAt(0)
                        .Elements<SdtBlock>().Where
                    (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.BATCHROLLBACK).Any())
                    {
                        rollBackTable.Elements<TableRow>().ElementAt(3)
                            .Elements<TableCell>().ElementAt(0)
                            .Elements<SdtBlock>().Where
                        (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.BATCHROLLBACK).Single().Remove();
                    }
                }

                #endregion

                #endregion

                #region Information Body

                SdtBlock informationBody = OpenXmlHelpers.GetContentControl(mainDocPart, ConstantBE.INFORMATIONBODY);

                //Get the table in the information body tag
                Table informationTable = informationBody.Descendants<Table>().Single();



                #region System Impact Checkbox

                //if we are delivering DB
                if (deliveryInfo.isDelvDB)
                {
                    Paragraph databaseSystemImpactPara = new Paragraph();

                    databaseSystemImpactPara = informationTable.Elements<TableRow>().ElementAt(3)
                            .Elements<TableCell>().ElementAt(2).Elements<Paragraph>().FirstOrDefault();


                    CheckBox databaseSysImpCB = databaseSystemImpactPara.Descendants<CheckBox>().FirstOrDefault();

                    if (databaseSysImpCB != null)
                    {
                        //get the checkbox
                        DefaultCheckBoxFormFieldState dbCB = databaseSysImpCB.Elements<DefaultCheckBoxFormFieldState>()
                        .FirstOrDefault();

                        //set it to true (Checked)
                        dbCB.Val = true;
                    }

                }

#warning check if true for all deliverables ex report !!

                if ((deliveryInfo.isDelvWEB) || (deliveryInfo.isDelvWS) || (deliveryInfo.isDelvBATCH) || (deliveryInfo.isDelvREPORT))
                {
                    Paragraph appSystemImpactPara = new Paragraph();

                    appSystemImpactPara = informationTable.Elements<TableRow>().ElementAt(5)
                            .Elements<TableCell>().ElementAt(0).Elements<Paragraph>().FirstOrDefault();


                    CheckBox appSysImpCB = appSystemImpactPara.Descendants<CheckBox>().FirstOrDefault();

                    if (appSysImpCB != null)
                    {
                        //get the checkbox
                        DefaultCheckBoxFormFieldState appCB = appSysImpCB.Elements<DefaultCheckBoxFormFieldState>()
                        .FirstOrDefault();

                        //set it to true (Checked)
                        appCB.Val = true;
                    }
                }


                if (deliveryInfo.isDelvK2PROCESS)
                {
                    Paragraph othersSystemImpactPara = new Paragraph();

                    othersSystemImpactPara = informationTable.Elements<TableRow>().ElementAt(5)
                            .Elements<TableCell>().ElementAt(2).Elements<Paragraph>().FirstOrDefault();

                    //set the text to K2 Server
                    othersSystemImpactPara.Descendants<SdtRun>().FirstOrDefault()
                        .Descendants<Text>().Single().Text = "K2 Server";

                    CheckBox othersSysImpCB = othersSystemImpactPara.Descendants<CheckBox>().FirstOrDefault();

                    if (othersSysImpCB != null)
                    {
                        //get the checkbox
                        DefaultCheckBoxFormFieldState othersCB = othersSysImpCB.Elements<DefaultCheckBoxFormFieldState>()
                        .FirstOrDefault();

                        //set it to true (Checked)
                        othersCB.Val = true;
                    }
                }

                #endregion


                //set values for HPSD ticket ID
                informationTable.Elements<TableRow>().ElementAt(12).Elements<SdtCell>()
                    .ElementAt(0).Descendants<Text>().Single().Text = rfc.HPSDTicketID;

                #endregion

                //save the changes to the document
                mainDocPart.Document.Save();

                //write the changes to disk
                using (StreamWriter sw = new StreamWriter(wordDoc.MainDocumentPart.GetStream(FileMode.Create)))
                {
                    sw.Write(mainDocPart);
                }

            }
        }
    }
}
