using System;
using System.Configuration;

namespace NeXT.DMT.Entities
{
    public static class ConstantBE
    {

        #region Paths


        /// <summary>
        /// Get the config path from the app setting 
        /// </summary>
        public static String CONFIGPATH
        {
            get
            {
                return ConfigurationManager.AppSettings["ConfigPath"];
            }
        }

        /// <summary>
        /// Get the template path from the app setting
        /// </summary>
        public static String TEMPLATEPATH
        {
            get
            {
                return ConfigurationManager.AppSettings["TemplatePath"];
            }
        }

        /// <summary>
        /// Get the output path
        /// </summary>
        public static String OUTPUTPATH
        {
            get
            {
                return ConfigurationManager.AppSettings["OutputPath"];
            }
        }

        #endregion

        #region Files

        /// <summary>
        /// Get the application list file name
        /// </summary>
        public static String APPLICATIONLISTFILE
        {
            get
            {
                return ConfigurationManager.AppSettings["ApplicationListFile"];
            }
        }


        /// <summary>
        /// Get the application list file name
        /// </summary>
        public static String DELIVERABLELISTFILE
        {
            get
            {
                return ConfigurationManager.AppSettings["DeliverableListFile"];
            }
        }


        /// <summary>
        /// Get the BL template file name
        /// </summary>
        public static String BLTEMPLATEFILE
        {
            get
            {
                return ConfigurationManager.AppSettings["BLTemplateFile"];
            }
        }

        /// <summary>
        /// Get the RFC template file name
        /// </summary>
        public static String RFCTEMPLATEFILE
        {
            get
            {
                return ConfigurationManager.AppSettings["RFCTemplateFile"];
            }
        }

        /// <summary>
        /// Get the person list file name
        /// </summary>
        public static String PERSONFILE
        {
            get
            {
                return ConfigurationManager.AppSettings["PersonListFile"];
            }
        }        

        #endregion

        #region Naming Convention


        /// <summary>
        /// Get the Internals Directory naming convention
        /// </summary>
        public static String INTERNALSDIRECTORYFORMAT
        {
            get
            {
                return ConfigurationManager.AppSettings["InternalsDirectoryFormat"];
            }
        }

        /// <summary>
        /// Get the Externals Directory naming convention
        /// </summary>
        public static String EXTERNALSDIRECTORYFORMAT
        {
            get
            {
                return ConfigurationManager.AppSettings["ExternalsDirectoryFormat"];
            }
        }

        /// <summary>
        /// Get the BL name convention
        /// </summary>
        public static String BLFORMAT
        {
            get
            {
                return ConfigurationManager.AppSettings["BLFormat"];
            }
        }

        /// <summary>
        /// Get the RFC name convention for INT and UAT
        /// </summary>
        public static String RFCFORMATINTANDUAT
        {
            get
            {
                return ConfigurationManager.AppSettings["RFCFormatIntAndUat"];
            }
        }

        /// <summary>
        /// Gets the RFC name convention for STA and PROD.
        /// </summary>
        public static String RFCFORMATSTAANDPROD
        {
            get
            {
                return ConfigurationManager.AppSettings["RFCFormatStaAndProd"];
            }
        }

        /// <summary>
        /// Get the TestScript name convention
        /// </summary>
        public static String TESTSCRIPTFORMAT
        {
            get
            {
                return ConfigurationManager.AppSettings["TestScriptFormat"];
            }
        }

        /// <summary>
        /// Gets the MCOPFORMAT.
        /// </summary>
        public static String MCOPFORMAT
        {
            get
            {
                return ConfigurationManager.AppSettings["McopFormat"];
            }
        }
        
        #endregion

        #region BL Document Tags

        /// <summary>
        /// The ITF body tag
        /// </summary>
        public static String ITFBODY
        {
            get
            {
                return "ITFBODY";
            }
        }

        /// <summary>
        /// The ITF Number tag
        /// </summary>
        public static String ITFNO
        {
            get
            {
                return "ITFNO";
            }
        }

        /// <summary>
        /// The Info body tag
        /// </summary>
        public static String INFOBODY
        {
            get
            {
                return "INFOBODY";
            }
        }
        /// <summary>
        /// The Prepared By tag
        /// </summary>
        public static String PREPAREDBY
        {
            get
            {
                return "PREPAREDBY";
            }
        }

        /// <summary>
        /// The Prepared Date tag
        /// </summary>
        public static String PREPAREDDATE
        {
            get
            {
                return "PREPAREDDATE";
            }
        }

        /// <summary>
        /// The Approved By tag
        /// </summary>
        public static String APPROVEDBY
        {
            get
            {
                return "APPROVEDBY";
            }
        }

        /// <summary>
        /// The Approved Date tag
        /// </summary>
        public static String APPROVEDDATE
        {
            get
            {
                return "APPROVEDDATE";
            }
        }

        /// <summary>
        /// The Reference tag
        /// </summary>
        public static String REFERENCE
        {
            get
            {
                return "REFERENCE";
            }
        }

        /// <summary>
        /// The Reference short description tag
        /// </summary>
        public static String REFERENCESHORTDESC
        {
            get
            {
                return "REFERENCESHORTDESC";
            }
        }
        
        /// <summary>
        /// The software body tag
        /// </summary>
        public static String SOFTWAREBODY
        {
            get
            {
                return "SOFTWAREBODY";
            }
        }

        /// <summary>
        /// The document body tag
        /// </summary>
        public static String DOCUMENTBODY
        {
            get
            {
                return "DOCUMENTBODY";
            }
        }
        
        #endregion

        #region RFC Document Tags

        #region Change Identification

        
        /// <summary>
        /// The CHANGEIDENTIFICATIONBODY body tag
        /// </summary>
        public static String CHANGEIDENTIFICATIONBODY
        {
            get
            {
                return "CHANGEIDENTIFICATIONBODY";
            }
        }

        /// <summary>
        /// The Entity tag
        /// </summary>
        public static String ENTITY
        {
            get
            {
                return "ENTITY";
            }
        }

        /// <summary>
        /// The SUBMITTEDBY tag
        /// </summary>
        public static String SUBMITTEDBY
        {
            get
            {
                return "SUBMITTEDBY";
            }
        }

        /// <summary>
        /// The SUBMITTEDBY tag
        /// </summary>
        public static String SUBMITTEDDATE
        {
            get
            {
                return "SUBMITTEDDATE";
            }
        }

        /// <summary>
        /// The EXPECTEDFINISHDATE tag
        /// </summary>
        public static String EXPECTEDFINISHDATE
        {
            get
            {
                return "EXPECTEDFINISHDATE";
            }
        }

        /// <summary>
        /// The EXPECTEDFINISHOUR tag
        /// </summary>
        public static String EXPECTEDFINISHOUR
        {
            get
            {
                return "EXPECTEDFINISHOUR";
            }
        }

        /// <summary>
        /// The EXPECTEDFINISDURATION tag
        /// </summary>
        public static String EXPECTEDFINISDURATION
        {
            get
            {
                return "EXPECTEDFINISDURATION";
            }
        }

        /// <summary>
        /// The CHANGEREASON tag
        /// </summary>
        public static String CHANGEREASON
        {
            get
            {
                return "CHANGEREASON";
            }
        }

        /// <summary>
        /// The ENVIRONMENT tag
        /// </summary>
        public static String ENVIRONMENT
        {
            get
            {
                return "ENVIRONMENT";
            }
        }

        /// <summary>
        /// The SERVERINFORMATION tag
        /// </summary>
        public static String SERVERINFORMATION
        {
            get
            {
                return "SERVERINFORMATION";
            }
        }

        /// <summary>
        /// The IMPACTONSERVICE tag
        /// </summary>
        public static String IMPACTONSERVICE
        {
            get
            {
                return "IMPACTONSERVICE";
            }
        }

        #endregion

        #region Backup And Maintainance

        /// <summary>
        /// The DESCRIPTIONREQUESTBODY body tag
        /// </summary>
        public static String DESCRIPTIONREQUESTBODY
        {
            get
            {
                return "DESCRIPTIONREQUESTBODY";
            }
        }

        /// <summary>
        /// The WEBBACKUPDIRECTIVE tag
        /// </summary>
        public static String WEBBACKUPDIRECTIVE
        {
            get
            {
                return "WEBBACKUPDIRECTIVE";
            }
        }

        /// <summary>
        /// The WSBACKUPDIRECTIVE tag
        /// </summary>
        public static String WSBACKUPDIRECTIVE
        {
            get
            {
                return "WSBACKUPDIRECTIVE";
            }
        }

        /// <summary>
        /// The DATABASEBACKUPDIRECTIVE tag
        /// </summary>
        public static String DATABASEBACKUPDIRECTIVE
        {
            get
            {
                return "DATABASEBACKUPDIRECTIVE";
            }
        }

        /// <summary>
        /// The K2BACKUPDIRECTIVE tag
        /// </summary>
        public static String K2BACKUPDIRECTIVE
        {
            get
            {
                return "K2BACKUPDIRECTIVE";
            }
        }

        /// <summary>
        /// The REPORTBACKUPDIRECTIVE tag
        /// </summary>
        public static String REPORTBACKUPDIRECTIVE
        {
            get
            {
                return "REPORTBACKUPDIRECTIVE";
            }
        }

        /// <summary>
        /// The BATCHBACKUPDIRECTIVE tag
        /// </summary>
        public static String BATCHBACKUPDIRECTIVE
        {
            get
            {
                return "BATCHBACKUPDIRECTIVE";
            }
        }


        #endregion

        #region Deployment

        /// <summary>
        /// Gets the SETUPDESCRIPTIONBODY tag in the RFC
        /// </summary>
        public static String SETUPDESCRIPTIONBODY
        {
            get
            {
                return "SETUPDESCRIPTIONBODY";
            }
        }


        /// <summary>
        /// Gets the WEBDEPLOYMENT tag in the RFC
        /// </summary>
        public static String WEBDEPLOYMENT
        {
            get
            {
                return "WEBDEPLOYMENT";
            }
        }

        /// <summary>
        /// Gets the WSDEPLOYMENT tag in the RFC
        /// </summary>
        public static String WSDEPLOYMENT
        {
            get
            {
                return "WSDEPLOYMENT";
            }
        }

        /// <summary>
        /// Gets the DATABASEDEPLOYMENT tag in the RFC
        /// </summary>
        public static String DATABASEDEPLOYMENT
        {
            get
            {
                return "DATABASEDEPLOYMENT";
            }
        }

        /// <summary>
        /// Gets the K2DEPLOYMENT tag in the RFC
        /// </summary>
        public static String K2DEPLOYMENT
        {
            get
            {
                return "K2DEPLOYMENT";
            }
        }


        /// <summary>
        /// Gets the REPORTDEPLOYMENT tag in the RFC
        /// </summary>
        public static String REPORTDEPLOYMENT
        {
            get
            {
                return "REPORTDEPLOYMENT";
            }
        }

        /// <summary>
        /// Gets the BATCHDEPLOYMENT tag in the RFC
        /// </summary>
        public static String BATCHDEPLOYMENT
        {
            get
            {
                return "BATCHDEPLOYMENT";
            }
        }

        /// <summary>
        /// Gets the WEBCONFIGMODIF tag in the RFC
        /// </summary>
        public static String WEBCONFIGMODIF
        {
            get
            {
                return "WEBCONFIGMODIF";
            }
        }

        /// <summary>
        /// Gets the PATHTOFILE.
        /// </summary>
        public static String PATHTOFILE
        {
            get
            {
                return "PATHTOFILE";
            }
        }

        /// <summary>
        /// Gets the FILENAME.
        /// </summary>
        public static String FILENAME
        {
            get
            {
                return "FILENAME";
            }
        }

        /// <summary>
        /// Gets the PATHTOSTORERESULT.
        /// </summary>
        public static String PATHTOSTORERESULT
        {
            get
            {
                return "PATHTOSTORERESULT";
            }
        }

        /// <summary>
        /// Gets the VERSIONCHANG e1.
        /// </summary>
        public static String VERSIONCHANGE1
        {
            get
            {
                return "VERSIONCHANGE1";
            }
        }

        /// <summary>
        /// Gets the VERSIONCHANG e2.
        /// </summary>
        public static String VERSIONCHANGE2
        {
            get
            {
                return "VERSIONCHANGE2";
            }
        }

        /// <summary>
        /// Gets the APPENVTAG.
        /// </summary>
        public static String APPENVTAG
        {
            get
            {
                return "APPENVTAG";
            }
        }




        #endregion

        #region RollBack

        /// <summary>
        /// Gets the TESTINGROLLBACKBODY.
        /// </summary>
        public static String TESTINGROLLBACKBODY
        {
            get
            {
                return "TESTINGROLLBACKBODY";
            }
        }

        /// <summary>
        /// Gets the REQUIREDTESTINGURL.
        /// </summary>
        public static String REQUIREDTESTINGURL
        {
            get
            {
                return "REQUIREDTESTINGURL";
            }
        }

        /// <summary>
        /// Gets the WEBROLLBACK.
        /// </summary>
        public static String WEBROLLBACK
        {
            get
            {
                return "WEBROLLBACK";
            }
        }

        /// <summary>
        /// Gets the WSROLLBACK.
        /// </summary>
        public static String WSROLLBACK
        {
            get
            {
                return "WSROLLBACK";
            }
        }

        /// <summary>
        /// Gets the DATABASEROLLBACK.
        /// </summary>
        public static String DATABASEROLLBACK
        {
            get
            {
                return "DATABASEROLLBACK";
            }
        }

        /// <summary>
        /// Gets the k2 ROLLBACK.
        /// </summary>
        public static String K2ROLLBACK
        {
            get
            {
                return "K2ROLLBACK";
            }
        }


        /// <summary>
        /// Gets the REPORTROLLBACK.
        /// </summary>
        public static String REPORTROLLBACK
        {
            get
            {
                return "REPORTROLLBACK";
            }
        }

        /// <summary>
        /// Gets the BATCHROLLBACK.
        /// </summary>
        public static String BATCHROLLBACK
        {
            get
            {
                return "BATCHROLLBACK";
            }
        }

        #endregion

        #region Information Body

        /// <summary>
        /// Gets the INFORMATIONBODY.
        /// </summary>
        public static String INFORMATIONBODY
        {
            get
            {
                return "INFORMATIONBODY";
            }
        }

        /// <summary>
        /// Gets the HPSDTICKETID.
        /// </summary>
        public static String HPSDTICKETID
        {
            get
            {
                return "HPSDTICKETID";
            }
        }
        
        #endregion
        
        #endregion
    }
}
