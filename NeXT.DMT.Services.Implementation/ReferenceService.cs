using System;
using System.Linq;
using System.Collections.ObjectModel;
using NeXT.DMT.DataAccess;
using NeXT.DMT.Entities;
using NeXT.DMT.Services.Contracts;
using NeXT.DMT.Entities.Servers;
using System.Text;

namespace NeXT.DMT.Services.Implementation
{

    public class ReferenceService : IReferenceService
    {

        #region Log4net

        /// <summary>
        /// Log4net manager instance
        /// </summary>
        static readonly log4net.ILog _Logger = log4net.LogManager.GetLogger(typeof(ReferenceService));
    
        #endregion

        #region Variables


        #endregion

        #region Web Service Methods
        /*
        public ApplicationConfigBE GetApplicationConfig(String appQuadri, String environment)
        {
            try
            {
                _Logger.Info("Trying GetApplicationConfig(String appQuadri, String environment)");

                return ApplicationDA.GetApplicationConfig(appQuadri, environment);
            }
            catch (Exception ex)
            {
                _Logger.ErrorFormat(String.Format("Error calling GetApplicationConfig({0})", appQuadri, environment));
                _Logger.Error(ex);
                throw ex;
            }
        }

        public void GenerateBL(BLInformationBE blInformation)
        {
            try
            {
                _Logger.Info("Trying GenerateBL(BLInformationBE blInformation)");

                BonDeLivraisonDA.GenerateBL(blInformation);
            }
            catch (Exception ex)
            {
                _Logger.ErrorFormat(String.Format("Error calling GenerateBL({0})", blInformation.ToString()));
                _Logger.Error(ex);
                throw ex;
            }
        }

        public void GenerateRFC(RequestForChangeBE rfc)
        {
            try
            {
                _Logger.Info("Trying GenerateRFC(RequestForChangeBE rfc)");

                RequestForChangeDA.GenerateRFC(rfc);
            }
            catch (Exception ex)
            {
                _Logger.ErrorFormat(String.Format("Error calling GenerateRFC({0})", rfc.ToString()));
                _Logger.Error(ex);
                throw ex;
            }
        }

        public Collection<DeliverableBE> GetAllDeliverables()
        {
            try
            {
                _Logger.Info("Trying GetAllDeliverables()");

                return DeliverableDA.GetAllDeliverables();
            }
            catch (Exception ex)
            {
                _Logger.ErrorFormat("Error calling GetAllDeliverables()");
                _Logger.Error(ex);
                throw ex;
            }
        }
        
        public PersonBE GetPersonDetailsByID(String personID)
        {
            try
            {
                _Logger.Info("Trying GetPersonDetailsByID(String personID)");

                return TransversalDA.GetPersonDetailsByID(personID);
            }
            catch (Exception ex)
            {
                _Logger.ErrorFormat(String.Format("Error calling GetPersonDetailsByID({0})", personID.ToString()));
                _Logger.Error(ex);
                throw ex;
            }
        }

         */

        public Collection<ApplicationBE> GetAllApplications()
        {
            try
            {
                _Logger.Info("Trying GetAllApplications()");

                return ApplicationDA.GetAllApplications();
            }
            catch (Exception ex)
            {
                _Logger.ErrorFormat("Error calling GetAllApplications()");
                _Logger.Error(ex);
                throw;
            }
        }

        public ApplicationBE GetApplication(String appQuadri)
        {
            try
            {
                _Logger.Info("Trying GetApplication(String appQuadri)");

                return ApplicationDA.GetApplication(appQuadri);
            }
            catch (Exception ex)
            {
                _Logger.ErrorFormat(String.Format("Error calling GetApplication({0})", appQuadri));
                _Logger.Error(ex);
                throw;
            }
        }


        public Collection<PersonBE> GetAllPersons()
        {
            try
            {
                _Logger.Info("Trying GetAllPersons()");

                return TransversalDA.GetAllPersons();
            }
            catch (Exception ex)
            {
                _Logger.ErrorFormat("Error calling GetAllPersons()");
                _Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// This function does some BLACK magic
        /// Go and get urself 
        /// 1. Some ruhm
        /// 2. A chicken (frozen also will do the job)
        /// 3. A broom
        /// </summary>
        /// <param name="jsonObject">The json object.</param>
        public void PerformTheMagic(JsonBE jsonObject)
        {
            try
            {
                _Logger.Info("Trying PerformTheMagic(JsonBE jsonObject)");

                //send the json object for processing
                this.ProcessJsonObject(jsonObject);

            }
            catch (Exception ex)
            {
                _Logger.ErrorFormat(String.Format("Error calling PerformTheMagic({0})", jsonObject.ToString()));
                _Logger.Error(ex);
                throw;
            }
        }
        
        #endregion
        
        #region Private Methods


        /// <summary>
        /// Processes the json object.
        /// Don't think your clever enought to optimise this function
        /// </summary>
        /// <param name="jsonObject">The json object.</param>
        private void ProcessJsonObject(JsonBE jsonObject)
        {

            DeliveryInfoBE deliveryInfo = new DeliveryInfoBE();

            deliveryInfo.AppQuadri = jsonObject.Step1.ApplicationQuadri;
            deliveryInfo.NewVersion = jsonObject.Step1.NewVersion;
            deliveryInfo.OldVersion = jsonObject.Step1.OldVersion;

            //determine on which environment the deployment will be made
            deliveryInfo.isINT = jsonObject.Step1.Environments.Where(env => env.Equals("INT")).Any();
            deliveryInfo.isUAT = jsonObject.Step1.Environments.Where(env => env.Equals("UAT")).Any();
            deliveryInfo.isSTA = jsonObject.Step1.Environments.Where(env => env.Equals("STA")).Any();
            deliveryInfo.isPROD = jsonObject.Step1.Environments.Where(env => env.Equals("PROD")).Any();

            //determine the deliverables
            deliveryInfo.isDelvWEB = jsonObject.Step1.Deliverables.Where(env => env.Equals("WEB")).Any();
#warning k2 ws also
            deliveryInfo.isDelvWS = jsonObject.Step1.Deliverables.Where(env => env.Equals("WS")).Any();
            deliveryInfo.isDelvDB = jsonObject.Step1.Deliverables.Where(env => env.Equals("DB")).Any();
            deliveryInfo.isDelvK2PROCESS = jsonObject.Step1.Deliverables.Where(env => env.Equals("K2PROCESS")).Any();
            deliveryInfo.isDelvREPORT = jsonObject.Step1.Deliverables.Where(env => env.Equals("REPORT")).Any();
            deliveryInfo.isDelvBATCH = jsonObject.Step1.Deliverables.Where(env => env.Equals("BATCH")).Any();

#warning should modify this logic
            if (jsonObject.Step1.NewVersion != jsonObject.Step1.OldVersion)
            {
                deliveryInfo.isModifWebConfig = true;
            }

            //get the application that we are making the deployment for
            ApplicationBE app = ApplicationDA.GetApplication(jsonObject.Step1.ApplicationQuadri);

            deliveryInfo.RootDirectoryPath = ConstantBE.OUTPUTPATH + '\\' + app.FolderName;
            deliveryInfo.ITFNo = jsonObject.Step1.ITFNumber;

            //if we are delivering on STAGING or PROD
            //we need to fetch the next external directory
            if (deliveryInfo.isSTA || deliveryInfo.isPROD)
            {
                deliveryInfo.NextExternalDirectory = DirectoryManagementDA.GetTheNextExternalDirectory(app, jsonObject.Step1.DeltaOrFull);
            }

            //we create the directory structure
            DirectoryManagementDA.CreateDirectoryStructure(deliveryInfo);

            String outputPath = String.Empty;

            #region BL Generation

            if (deliveryInfo.isINT || deliveryInfo.isUAT)
            {
                outputPath = deliveryInfo.RootDirectoryPath + '\\' + "Internals" + '\\' +
                    "ITF" + deliveryInfo.ITFNo + '\\';

                BonDeLivraisonDA.GenerateBL(GetBLFromJsonObject(jsonObject, "Internals",
                    deliveryInfo.NextExternalDirectory), outputPath);
            }

            if (deliveryInfo.isSTA || deliveryInfo.isPROD)
            {
                outputPath = deliveryInfo.RootDirectoryPath + '\\' + "Externals" + '\\' +
                    deliveryInfo.NextExternalDirectory + '\\';

                BonDeLivraisonDA.GenerateBL(GetBLFromJsonObject(jsonObject, "Externals",
                    deliveryInfo.NextExternalDirectory), outputPath);
            }

            #endregion

            #region RFC Generation

            foreach (String item in jsonObject.Step1.Environments)
            {
                ApplicationConfigBE appConfig = ApplicationDA.GetApplicationConfig(jsonObject.Step1.ApplicationQuadri, item);

                RequestForChangeDA.GenerateRFC(GetRFCFromJsonObject(jsonObject, item, jsonObject.Step1.ApplicationQuadri,
                    deliveryInfo), deliveryInfo, item, appConfig);
            }

            #endregion

            #region MCOP Generation

            foreach (String item in jsonObject.Step1.Environments)
            {
                ApplicationConfigBE appConfig = ApplicationDA.GetApplicationConfig(jsonObject.Step1.ApplicationQuadri, item);

                if (deliveryInfo.isDelvWEB)
                {
                    McopDA.GenerateMcopFileForSetup(GetMCOPFromJsonObject(appConfig, deliveryInfo, "WEB", true, item), deliveryInfo, item, "WEB");
                    McopDA.GenerateMcopFileForRollBack(GetMCOPFromJsonObject(appConfig, deliveryInfo, "WEB", false, item), deliveryInfo, item, "WEB");
                }

                if (deliveryInfo.isDelvWS)
                {
                    McopDA.GenerateMcopFileForSetup(GetMCOPFromJsonObject(appConfig, deliveryInfo, "WS", true, item), deliveryInfo, item, "WS");
                    McopDA.GenerateMcopFileForRollBack(GetMCOPFromJsonObject(appConfig, deliveryInfo, "WS", false, item), deliveryInfo, item, "WS");
                }

                if (deliveryInfo.isDelvBATCH)
                {
                    McopDA.GenerateMcopFileForSetup(GetMCOPFromJsonObject(appConfig, deliveryInfo, "BATCH", true, item), deliveryInfo, item, "BATCH");
                    McopDA.GenerateMcopFileForRollBack(GetMCOPFromJsonObject(appConfig, deliveryInfo, "BATCH", false, item), deliveryInfo, item, "BATCH");
                }
            }


            #endregion

        }


        /// <summary>
        /// Get our BL object from the JSON object
        /// </summary>
        /// <param name="jsonObject">The json object.</param>
        /// <returns></returns>
        private BLInformationBE GetBLFromJsonObject(JsonBE jsonObject, String delvType, String nextExternalDirectory)
        {

            BLInformationBE bonDeLivraison = new BLInformationBE();

            ApplicationBE application = new ApplicationBE();

            //assigned the application quadri
            //it is the only variable that is used for the BL
            //no need to assigned the others
            application.Quadri = jsonObject.Step1.ApplicationQuadri;

            bonDeLivraison.App = application;

            //parse the ITF to number
            bonDeLivraison.IFT = int.Parse(jsonObject.Step1.ITFNumber);
            
            //get the person name and assigned it to the object
            bonDeLivraison.PreparedBy = TransversalDA.GetPersonDetailsByID(jsonObject.Step2.PreparedBy).name;
            bonDeLivraison.ApprovedBy = TransversalDA.GetPersonDetailsByID(jsonObject.Step2.ApprovedBy).name;

            //parse the delivery date date
            DateTime deliveryDate = DateTime.Parse(jsonObject.Step2.DeliveryDate);

            //both dates will be the same !!
            bonDeLivraison.PreparedDate = deliveryDate;
            bonDeLivraison.ApprovedDate = deliveryDate;

            Collection<BLDeliverableBE> bonDeLivraisonDeliverableCol = new Collection<BLDeliverableBE>();

            int countDeliverables = 1;

            foreach (String item in jsonObject.Step1.SVNLinkAndVersion)
            {
                //split the string to an array to get the
                //other items
                String[] splitLinkAndVersionItem = item.Split(';');

                BLDeliverableBE bonDeLivraisonDeliverable = new BLDeliverableBE();

#warning add version
                bonDeLivraisonDeliverable.ID = countDeliverables;
                bonDeLivraisonDeliverable.Name = splitLinkAndVersionItem[0];
                bonDeLivraisonDeliverable.Etiquette = splitLinkAndVersionItem[1];

                //only for those two we don't put version number
                if ((bonDeLivraisonDeliverable.Name == "DB") || (bonDeLivraisonDeliverable.Name == "K2PROCESS"))
                {
                    bonDeLivraisonDeliverable.Version = String.Empty;
                }
                else
                {
                    bonDeLivraisonDeliverable.Version = jsonObject.Step1.NewVersion.Substring(0, 5);
                }
                
                
                //bonDeLivraisonDeliverable.Version = splitLinkAndVersionItem[2];
                bonDeLivraisonDeliverable.Location = DirectoryManagementDA.GetDistributionDirectoryPath(application.Quadri, delvType,
                    bonDeLivraisonDeliverable.Name, bonDeLivraison.IFT.ToString(), nextExternalDirectory);

                bonDeLivraisonDeliverableCol.Add(bonDeLivraisonDeliverable);

                countDeliverables++;
            }


            String[] splitChangeAndQCItem = jsonObject.Step2.ChangeQCAndDescription.Split(';');

            bonDeLivraison.Reference = splitChangeAndQCItem[0];
            bonDeLivraison.ReferenceDescription = splitChangeAndQCItem[1];

            //assigned the deliverable collection
            bonDeLivraison.BLSoftwareDeliverableCol = bonDeLivraisonDeliverableCol;

            Collection<BLDocumentationBE> bonDeLivraisonDocumentationCol = new Collection<BLDocumentationBE>();

            int countDocDeliverables = 1;

            foreach (String item in jsonObject.Step2.Documentation)
            {
                //split the string to an array to get the
                //other items
                String[] splitLinkAndVersionItem = item.Split(';');

                BLDocumentationBE bonDeLivraisonDoc = new BLDocumentationBE();

                bonDeLivraisonDoc.ID = countDocDeliverables;
                bonDeLivraisonDoc.DocName = splitLinkAndVersionItem[0];
                bonDeLivraisonDoc.Location = splitLinkAndVersionItem[1];
                bonDeLivraisonDoc.Remark = splitLinkAndVersionItem[2];

                //we are changin version
                if (jsonObject.Step1.NewVersion != jsonObject.Step1.OldVersion)
                {
                    bonDeLivraisonDoc.Version = jsonObject.Step1.NewVersion.Substring(0, 5);
                }
                else
                {
                    bonDeLivraisonDoc.Version = "1.0.0";
                }

                bonDeLivraisonDocumentationCol.Add(bonDeLivraisonDoc);

                countDocDeliverables++;
            }

            //assigned the document collection
            bonDeLivraison.BLDocumentDeliverableCol = bonDeLivraisonDocumentationCol;

            return bonDeLivraison;
        }


        /// <summary>
        /// Gets the RFC from json object.
        /// </summary>
        /// <param name="jsonObject">The json object.</param>
        /// <returns></returns>
        private RequestForChangeBE GetRFCFromJsonObject(JsonBE jsonObject, String environment, String appQuadri, DeliveryInfoBE deliveryInfo)
        {
            RequestForChangeBE rfc = new RequestForChangeBE();

            //the IS Entity
            //rfc.Entity = jsonObject.Step3.ISEntity;
#warning should be crosschecked
            if ((environment == "INT") || (environment == "UAT"))
            {
                rfc.Entity = "C2IL";
            }
            else
            {
                rfc.Entity = "COS";
            }
            
            //who submitted the rfc
            rfc.SubmittedBy = TransversalDA.GetPersonDetailsByID(jsonObject.Step3.SubmittedBy).name;

            //the submitted date
            rfc.SubmittedDate = DateTime.Parse(jsonObject.Step3.SubmittedDate);

            //qualification fo request
            rfc.QualificationOfRequest = jsonObject.Step3.QualificationOfRequest;

            //priority
            rfc.Priority = jsonObject.Step3.Priority;

            //regulated environment
            rfc.RegulatedEnvironment = jsonObject.Step3.RegulatedEnvironment;

            //only set the expected date / time for
            //INT and UAT environment
            if (environment == "INT" || environment == "UAT")
            {
                rfc.ExpectedDate = DateTime.Parse(jsonObject.Step3.ExpectedDate).ToString("dd/MM/yy");
                rfc.ExpectedStartTime = jsonObject.Step3.ExpectedStartTime;
                rfc.ExpectedDuration = jsonObject.Step3.ExpectedDuration;
            }
            else
            {
                rfc.ExpectedDate = "To be set";
                rfc.ExpectedStartTime = "To be set";
                rfc.ExpectedDuration = jsonObject.Step3.ExpectedDuration;
            }

            //change reason
            rfc.ChangeReason = jsonObject.Step3.ChangeReason + " on " + environment ;

            //the deployment environment
            rfc.Environment = environment;

            //HPSD ID

            String[] splitChangeAndQCItem = jsonObject.Step2.ChangeQCAndDescription.Split(';');

            rfc.HPSDTicketID = splitChangeAndQCItem[0].Replace("|", " / ");

            //fetch the application configs base on the environment
            ApplicationConfigBE applicationConfigurations = ApplicationDA.GetApplicationConfig(appQuadri, environment);

            //initilise the string builder for the servers environments
            StringBuilder strBuilderEnvironment = new StringBuilder();

            if (deliveryInfo.isDelvWEB)
            {
                strBuilderEnvironment.Append("WEB :");

                foreach (WebServer item in applicationConfigurations.WebServers)
                {
                    //not he last item in the collection
                    if (item != applicationConfigurations.WebServers.Last())
                    {
                        strBuilderEnvironment.Append(" ").Append(item.Name).Append(" / ");
                    }

                    else
                    {
                        strBuilderEnvironment.Append(" ").Append(item.Name);
                    }
                }

                strBuilderEnvironment.Append("|");
            }

            if (deliveryInfo.isDelvWS)
            {
                strBuilderEnvironment.Append("WS :");

                foreach (WebServiceServer item in applicationConfigurations.WebServiceServers)
                {
                    //not he last item in the collection
                    if (item != applicationConfigurations.WebServiceServers.Last())
                    {
                        strBuilderEnvironment.Append(" ").Append(item.Name).Append(" / ");
                    }

                    else
                    {
                        strBuilderEnvironment.Append(" ").Append(item.Name);
                    }
                }

                strBuilderEnvironment.Append("|");
            }

            if (deliveryInfo.isDelvDB)
            {
                strBuilderEnvironment.Append("DB :");

                foreach (DatabaseServer item in applicationConfigurations.DatabaseServers)
                {
                    //not he last item in the collection
                    if (item != applicationConfigurations.DatabaseServers.Last())
                    {
                        strBuilderEnvironment.Append(" ").Append(item.Name).Append(" / ");
                    }

                    else
                    {
                        strBuilderEnvironment.Append(" ").Append(item.Name);
                    }
                }

                strBuilderEnvironment.Append("|");
            }

            if (deliveryInfo.isDelvK2PROCESS)
            {
                strBuilderEnvironment.Append("K2 :");

                foreach (WorkflowServer item in applicationConfigurations.WorkflowServers)
                {
                    //not he last item in the collection
                    if (item != applicationConfigurations.WorkflowServers.Last())
                    {
                        strBuilderEnvironment.Append(" ").Append(item.Name).Append(" / ");
                    }

                    else
                    {
                        strBuilderEnvironment.Append(" ").Append(item.Name);
                    }
                }

                strBuilderEnvironment.Append("|");
            }

            if (deliveryInfo.isDelvREPORT)
            {
                strBuilderEnvironment.Append("REPORT :");

                foreach (ReportingServer item in applicationConfigurations.ReportingServers)
                {
                    //not he last item in the collection
                    if (item != applicationConfigurations.ReportingServers.Last())
                    {
                        strBuilderEnvironment.Append(" ").Append(item.Name).Append(" / ");
                    }

                    else
                    {
                        strBuilderEnvironment.Append(" ").Append(item.Name);
                    }
                }

                strBuilderEnvironment.Append("|");
            }

            if (deliveryInfo.isDelvBATCH)
            {
                strBuilderEnvironment.Append("BATCH :");

                foreach (BatchServer item in applicationConfigurations.BatchServers)
                {
                    //not he last item in the collection
                    if (item != applicationConfigurations.BatchServers.Last())
                    {
                        strBuilderEnvironment.Append(" ").Append(item.Name).Append(" / ");
                    }

                    else
                    {
                        strBuilderEnvironment.Append(" ").Append(item.Name);
                    }
                }

                strBuilderEnvironment.Append("|");
            }

            rfc.ServerInformation = strBuilderEnvironment.ToString();

            return rfc;
        }


        private McopBE GetMCOPFromJsonObject(ApplicationConfigBE appConfig, 
            DeliveryInfoBE deliveryInfo, String deliverable, bool isSetup, String environment)
        {
            McopBE mcop = new McopBE();

            #region Servers 

            Collection<String> serversCol = new Collection<string>();

            if (deliverable == "WEB")
            {
                foreach (WebServer item in appConfig.WebServers)
                {
                    serversCol.Add(item.Name);
                }
            }
            else if (deliverable == "WS")
            {
                foreach (WebServiceServer item in appConfig.WebServiceServers)
                {
                    serversCol.Add(item.Name);
                }
            }
            else
            {
                foreach (BatchServer item in appConfig.BatchServers)
                {
                    serversCol.Add(item.Name);
                }
            }

            //set the server path
            mcop.ServersCol = serversCol;

            #endregion

            //set long date format to be true
            //you can change the format by putting
            //it to false
            mcop.isLongIsoDateDefault = true;

            //setup or rollback
            if (isSetup)
            {
                mcop.FirstAction = "Copy";
                mcop.SecondAction = "Copy";

                mcop.isNewFirstAction = true;
                mcop.isNewSecondAction = true;

                mcop.isOverWriteFirstAction = true;
                mcop.isOverWriteSecondAction = true;

                mcop.PhysicalPath = "\\\\{s}" + appConfig.WEBPhysicalLocation;

                mcop.BackupPath = DirectoryManagementDA.GetDirectoryPathForMCOP(deliveryInfo, environment, deliverable, false) + "\\{s}";
                mcop.DeliverablePath = DirectoryManagementDA.GetDirectoryPathForMCOP(deliveryInfo, environment, deliverable, true);
            }
            else
            {
                mcop.FirstAction = "Del";
                mcop.SecondAction = "Copy";

                mcop.isNewFirstAction = false;
                mcop.isNewSecondAction = false;

                mcop.isOverWriteFirstAction = false;
                mcop.isOverWriteSecondAction = true;

                mcop.PhysicalPath = "\\\\{s}" + appConfig.WEBPhysicalLocation;

                mcop.BackupPath = DirectoryManagementDA.GetDirectoryPathForMCOP(deliveryInfo, environment, deliverable, false) + "\\{s}";
                mcop.DeliverablePath = DirectoryManagementDA.GetDirectoryPathForMCOP(deliveryInfo, environment, deliverable, true);
            }

            return mcop;
        }

        #endregion
    }
}
