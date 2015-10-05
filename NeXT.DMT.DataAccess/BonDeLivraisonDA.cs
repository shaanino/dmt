using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NeXT.DMT.Entities;
using DocumentFormat.OpenXml.Packaging;
using System.IO;
using DocumentFormat.OpenXml.Wordprocessing;
using NeXT.DMT.Utilities;

namespace NeXT.DMT.DataAccess
{
    public static class BonDeLivraisonDA
    {
        /// <summary>
        /// Generate a new Bon De Livraison Document
        /// Step1.Get the BL template
        /// Step2.Copy it to its new location
        /// Step3.Rename it
        /// Step4.Insert all required values
        /// </summary>
        /// <param name="blInformation">The bon de livraison parameters</param>
        /// <param name="outputPath">The output path.</param>
        public static void GenerateBL(BLInformationBE blInformation, String outputPath)
        {

            //path to the bl template
            String blTemplatePath = ConstantBE.TEMPLATEPATH + '\\' + ConstantBE.BLTEMPLATEFILE;

            //the BL name Ex. BL_IPRI_ITF587_20120328
            //BL_{0}_{1}_{2}
            String blNewName = String.Format(ConstantBE.BLFORMAT, 
                blInformation.App.Quadri, 
                "ITF" + blInformation.IFT,
                blInformation.ApprovedDate.ToString("yyyyMMdd"));

            //build the output path
            /*String outputPath = ContantsBE.OUTPUTPATH + 
                "ITF" + blInformation.IFT + "\\" + 
                blNewName + ".docx";
            */

            outputPath += blNewName + ".docx";

            //copy the bl template to a new location and rename it accordingly
            DirectoryManagementDA.CopyFileAndRename(blTemplatePath, outputPath);

            

            //open the copied file in its new location for modification
            using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(outputPath, true))
            {
                //get the document body
                MainDocumentPart mainDocPart = wordDoc.MainDocumentPart;

                #region ITF

                //get only the ITF body from the document
                SdtBlock itfBody = OpenXmlHelpers.GetContentControl(mainDocPart, ConstantBE.ITFBODY);

                //Get the table in the itf info tag
                Table itfBodyTable = itfBody.Descendants<Table>().Single();

                //As the TAG is in a table we have to get the table element first.
                //then filter further down to get the element
                SdtRun itfTagRR = itfBodyTable.Elements<TableRow>().FirstOrDefault()
                                                .Elements<TableCell>().FirstOrDefault()
                                                .Elements<Paragraph>().FirstOrDefault()
                                                .Elements<SdtRun>().Where
                        (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.ITFNO).Single();

                //set the ITF number
                itfTagRR.Descendants<Text>().Single().Text = blInformation.IFT.ToString();

                #endregion

                #region Info


                //get only the Info body from the document
                SdtBlock infoBody = OpenXmlHelpers.GetContentControl(mainDocPart, ConstantBE.INFOBODY);

                //Get the table in the info tag
                Table infoBodyTable = infoBody.Descendants<Table>().Single();

                #region Row 1

                SdtRun prepareByTagRR = infoBodyTable.Elements<TableRow>().ElementAt(0)
                                       .Elements<TableCell>().ElementAt(0)
                                       .Elements<Paragraph>().FirstOrDefault()
                                       .Elements<SdtRun>().Where
                        (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.PREPAREDBY).Single();

                //set the prepared by text
                prepareByTagRR.Descendants<Text>().Single().Text = blInformation.PreparedBy;


                SdtRun prepareDateTagRR = infoBodyTable.Elements<TableRow>().ElementAt(0)
                                                         .Elements<TableCell>().ElementAt(1)
                                                         .Elements<Paragraph>().FirstOrDefault()
                                                         .Elements<SdtRun>().Where
                        (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.PREPAREDDATE).Single();

                //set the prepared date text
                prepareDateTagRR.Descendants<Text>().Single().Text = blInformation.PreparedDate.ToString("dd/MM/yyyy");

                #endregion

                #region Row 2

                SdtRun approvedByTagRR = infoBodyTable.Elements<TableRow>().ElementAt(1)
                                        .Elements<TableCell>().ElementAt(0)
                                        .Elements<Paragraph>().FirstOrDefault()
                                        .Elements<SdtRun>().Where
                        (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.APPROVEDBY).Single();

                //set the approved by text
                approvedByTagRR.Descendants<Text>().Single().Text = blInformation.ApprovedBy;


                SdtRun approvedDateTagRR = infoBodyTable.Elements<TableRow>().ElementAt(1)
                                                         .Elements<TableCell>().ElementAt(1)
                                                         .Elements<Paragraph>().FirstOrDefault()
                                                         .Elements<SdtRun>().Where
                        (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.APPROVEDDATE).Single();

                //set the approved date text
                approvedDateTagRR.Descendants<Text>().Single().Text = blInformation.ApprovedDate.ToString("dd/MM/yyyy");

                #endregion

                #region Row 3


                SdtRun referenceTagRR = infoBodyTable.Elements<TableRow>().ElementAt(2)
                                                        .Elements<TableCell>().ElementAt(0)
                                                        .Elements<Paragraph>().FirstOrDefault()
                                                        .Elements<SdtRun>().Where
                        (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.REFERENCE).Single();

                //remove the vertical bar with blank space and set the reference text
                referenceTagRR.Descendants<Text>().Single().Text = blInformation.Reference.Replace('|', ' ');


                // if you want the each reference on each line just uncomment the following codes

                /*
                //split the string
                String[] splitedReference = blInformation.Reference.Split('|');


                //set the reference text
                foreach (String item in splitedReference)
                {
                    Run newRun = new Run();

                    //add a new line and the append the item
                    newRun.AppendChild(new Break());
                    newRun.AppendChild(new Text(item));

                    referenceTagRR.SdtContentRun.Append(newRun);

                }
                */

                #endregion

                #region Row 4

                //find the reference description tag
                SdtRun referenceShortDescTagRR = infoBodyTable.Elements<TableRow>().ElementAt(3)
                                        .Elements<TableCell>().ElementAt(0)
                                        .Elements<Paragraph>().FirstOrDefault()
                                        .Elements<SdtRun>().Where
                        (r => r.SdtProperties.GetFirstChild<Tag>().Val == ConstantBE.REFERENCESHORTDESC).Single();

                //clear the tag
                referenceShortDescTagRR.Descendants<Text>().Single().Text = " ";

                //split the string
                String[] splitedRefDescription = blInformation.ReferenceDescription.Split('|');

                //set the reference short description text
                foreach (String item in splitedRefDescription)
                {
                    Run newRun = new Run();

                    //add a new line and the append the item
                    newRun.AppendChild(new Break());
                    newRun.AppendChild(new Text(item));

                    referenceShortDescTagRR.SdtContentRun.Append(newRun);

                }

                //referenceShortDescTagRR.Descendants<Text>().Single().Text = blInformation.ReferenceDescription;

                #endregion

                #endregion

                #region Software

                //get only the software body from the document
                SdtBlock softwareBody = OpenXmlHelpers.GetContentControl(mainDocPart, ConstantBE.SOFTWAREBODY);

                //Get the table in the software tag
                Table softwareBodyTable = softwareBody.Descendants<Table>().Single();

                TableRow softRow = softwareBodyTable.Elements<TableRow>().Last();

                foreach (BLDeliverableBE item in blInformation.BLSoftwareDeliverableCol)
                {
                    TableRow rowCopy = (TableRow)softRow.CloneNode(true);

                    //No
                    rowCopy.Descendants<TableCell>().ElementAt(0).Elements<Paragraph>().First()
                        .Append((new Run(new Text(item.ID.ToString()))));

                    //Nom du livrable
                    rowCopy.Descendants<TableCell>().ElementAt(1).Elements<Paragraph>().First()
                        .Append((new Run(new Text(item.Name))));

                    if ((!String.IsNullOrEmpty(item.Version)) && (item.Version.Count() == 7))
                    {
                        //Version - truncate it to 3 digit Ex 2.0.2.1 to 2.0.2
                        rowCopy.Descendants<TableCell>().ElementAt(2).Elements<Paragraph>().First()
                            .Append((new Run(new Text(item.Version.Substring(0, 5)))));
                    }
                    else
                    {
                        rowCopy.Descendants<TableCell>().ElementAt(2).Elements<Paragraph>().First()
                            .Append((new Run(new Text(item.Version))));
                    }

                    //Emplacement Distribution
                    rowCopy.Descendants<TableCell>().ElementAt(3).Elements<Paragraph>().First()
                        .Append((new Run(new Text(item.Location))));

                    //Emplacement de l’étiquette
                    rowCopy.Descendants<TableCell>().ElementAt(4).Elements<Paragraph>().First()
                        .Append((new Run(new Text(item.Etiquette))));

                    softwareBodyTable.AppendChild(rowCopy);

                }

                softwareBodyTable.RemoveChild(softRow);

                #endregion

                #region Documentation

                //get only the document body from the document
                SdtBlock documentBody = OpenXmlHelpers.GetContentControl(mainDocPart, ConstantBE.DOCUMENTBODY);

                //Get the table in the document tag
                Table documentBodyTable = documentBody.Descendants<Table>().Single();

                TableRow docRow = documentBodyTable.Elements<TableRow>().Last();

                foreach (BLDocumentationBE item in blInformation.BLDocumentDeliverableCol)
                {
                    TableRow rowCopy = (TableRow)docRow.CloneNode(true);

                    //No
                    rowCopy.Descendants<TableCell>().ElementAt(0).Elements<Paragraph>().First()
                        .Append((new Run(new Text(item.ID.ToString()))));

                    //Document
                    rowCopy.Descendants<TableCell>().ElementAt(1).Elements<Paragraph>().First()
                        .Append((new Run(new Text(item.DocName))));

                    //Version
                    rowCopy.Descendants<TableCell>().ElementAt(2).Elements<Paragraph>().First()
                        .Append((new Run(new Text(item.Version))));

                    //Emplacement
                    rowCopy.Descendants<TableCell>().ElementAt(3).Elements<Paragraph>().First()
                        .Append((new Run(new Text(item.Location))));

                    //Remarques
                    rowCopy.Descendants<TableCell>().ElementAt(4).Elements<Paragraph>().First()
                        .Append((new Run(new Text(item.Remark))));

                    documentBodyTable.AppendChild(rowCopy);

                }

                documentBodyTable.RemoveChild(docRow);

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
