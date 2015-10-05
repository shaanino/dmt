using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NeXT.DMT.DataAccess;
using NeXT.DMT.Services.Contracts;
using NeXT.DMT.Entities;
using System.Collections.ObjectModel;
using System.ServiceModel.Activation;
using System.ServiceModel;

namespace NeXT.DMT.Services.Implementation
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class AjaxService : IAjaxService
    {
        static readonly log4net.ILog _Logger = log4net.LogManager.GetLogger(typeof(ReferenceService));

        public Collection<ApplicationBE> GetAllApplications()
        {
            try
            {
                _Logger.Info("Trying GetAllApplications()");


                McopBE test = new McopBE();
                McopBE test2 = new McopBE();

                Collection<String> serverCol = new Collection<string>();

                serverCol.Add("GTAQ4WMOSS1");
                serverCol.Add("GTAQ4WMOSS2");
                serverCol.Add("GTAQ4WMOSS3");
                serverCol.Add("GTAQ4WMOSS4");
                test.ServersCol = serverCol;
                test.isLongIsoDateDefault = true;

                test.PhysicalPath = @"D$\Intranet\Apps\WOLF_PROM\WEBSERVICES";
                test.BackupPath = @"\\cse11-dfsserv1.pharma.aventis.com\FileService\Factories_Dev\Livraisons\PROM_(MASP)\Externals\PROM_Liv_4_(DELTA)\Backups\STA\{s}";
                test.FirstAction = "Copy";
                test.isNewFirstAction = true;
                test.isOverWriteFirstAction = true;

                test.DeliverablePath = @"\\cse11-dfsserv1.pharma.aventis.com\FileService\Factories_Dev\Livraisons\PROM_(MASP)\Externals\PROM_Liv_4_(DELTA)\WS\Webservices";
                test.SecondAction = "Copy";
                test.isNewSecondAction = true;
                test.isOverWriteSecondAction = true;

                McopDA.GenerateMcopFileForSetup(test);


                test2.ServersCol = serverCol;
                test2.isLongIsoDateDefault = true;

                test2.PhysicalPath = @"D$\Intranet\Apps\WOLF_PROM\WEBSERVICES";
                test2.BackupPath = @"\\cse11-dfsserv1.pharma.aventis.com\FileService\Factories_Dev\Livraisons\PROM_(MASP)\Externals\PROM_Liv_4_(DELTA)\Backups\STA\{s}";
                test2.FirstAction = "Del";

                test2.SecondAction = "Copy";
                test2.isNewSecondAction = false;
                test2.isOverWriteSecondAction = true;

                McopDA.GenerateMcopFileForRollBack(test2);

                return ApplicationDA.GetAllApplications();
            }
            catch (Exception ex)
            {
                _Logger.Error("Error calling GetAllApplications()",ex);
                throw ex;
            }
        }

        public void GenerateBL(BLInformationBE blInformation)
        {
            try
            {
                _Logger.Info("Trying GenerateBL(BLInformationBE blInformation)");

                DirectoryManagementDA.GetTheNextExternalDirectory(blInformation.App, "DELTA");
                //BonDeLivraisonDA.GenerateBL(blInformation);
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

                //RequestForChangeDA.GenerateRFC(rfc);
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
    }
}
