using System;
using System.Collections.ObjectModel;
using System.Xml;
using System.Linq;
using NeXT.DMT.Entities;
using NeXT.DMT.Entities.Servers;


namespace NeXT.DMT.DataAccess
{
    public static class ApplicationDA
    {
        /// <summary>
        /// Fetch all the applications from the XML file
        /// </summary>
        /// <returns></returns>
        public static Collection<ApplicationBE> GetAllApplications()
        {
            //get the xml file path
            String xmlPath = ConstantBE.CONFIGPATH + '\\' + ConstantBE.APPLICATIONLISTFILE;

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.Load(xmlPath);

            //retrive all  the nodes having tag "Application"
            XmlNodeList xmlNodeList = xmlDoc.SelectNodes("//Applications/Application");

            Collection<ApplicationBE> applicationCol = new Collection<ApplicationBE>();

            foreach (XmlElement item in xmlNodeList)
            {
                ApplicationBE newApp = new ApplicationBE();

                newApp.ID = int.Parse(item.ChildNodes[0].InnerText);
                newApp.Name = item.ChildNodes[1].InnerText;

                //convert it to upper in case it is not in the xml file
                newApp.Quadri = item.ChildNodes[2].InnerText.ToUpper();

                newApp.FolderName = item.ChildNodes[3].InnerText;

                Collection<String> delvCol = new Collection<string>();

                //loop for all deliverables
                foreach (XmlElement delvItem in item.ChildNodes[4])
                {
                    delvCol.Add(delvItem.InnerText);
                }

                newApp.Deliverables = delvCol;

                applicationCol.Add(newApp);
            }

            return applicationCol;
        }

        /// <summary>
        /// Fetch an applications from the XML file
        /// based on the app quadri
        /// </summary>
        /// <returns></returns>
        public static ApplicationBE GetApplication(String appQuadri)
        {
            return GetAllApplications().Where(app => app.Quadri.Equals(appQuadri))
                                       .FirstOrDefault();
        }
        
        /// <summary>
        /// Fetch the application config based on the application name and the environment
        /// </summary>
        /// <param name="app">The application</param>
        /// <param name="environment">Which environment?</param>
        /// <returns></returns>
        public static ApplicationConfigBE GetApplicationConfig(String appQuadri, String environment)
        {
            //get the xml file path based APPLICATION
            String xmlPath = ConstantBE.CONFIGPATH + '\\' + appQuadri + ".xml";

            XmlDocument xmlDoc = new XmlDocument();

            //load the xml file
            xmlDoc.Load(xmlPath);

            ApplicationConfigBE appConfig = new ApplicationConfigBE();

            //based on the ENVIRONMENT we retrive all the nodes having tag "Info"
            XmlNodeList infoXmlNodeList = xmlDoc.SelectNodes("//" + appQuadri + "/" + environment + "/Info");

            appConfig.AppSettingEnvironment = infoXmlNodeList[0]["AppSettingEnv"].InnerText;
            appConfig.AppTestUrl = infoXmlNodeList[0]["AppTestUrl"].InnerText;
            appConfig.WEBPhysicalLocation = infoXmlNodeList[0]["WEBPhysicalLocation"].InnerText;
            appConfig.WSPhysicalLocation = infoXmlNodeList[0]["WSPhysicalLocation"].InnerText;
            appConfig.K2WSPhysicalLocation = infoXmlNodeList[0]["K2WSPhysicalLocation"].InnerText;

            #region WEB Nodes

            //based on the ENVIRONMENT we retrive all web servers
            XmlNodeList webserverXmlNodeList = xmlDoc.SelectNodes("//" + appQuadri + "/" + environment + "/Servers/WebServers/*");

            if (webserverXmlNodeList.Count != 0)
            {

                Collection<WebServer> webserverCol = new Collection<WebServer>();

                foreach (XmlElement item in webserverXmlNodeList)
                {
                    WebServer ws = new WebServer();

                    ws.ID = int.Parse(item.ChildNodes[0].InnerText);
                    ws.Name = item.ChildNodes[1].InnerText;

                    webserverCol.Add(ws);
                }

                appConfig.WebServers = webserverCol;
            }

            #endregion

            #region WS Nodes

            //based on the ENVIRONMENT we retrive all web service servers
            XmlNodeList webserviceserverXmlNodeList = xmlDoc.SelectNodes("//" + appQuadri + "/" + environment + "/Servers/WebServiceServers/*");

            if (webserviceserverXmlNodeList.Count != 0)
            {
                Collection<WebServiceServer> webserviceServerCol = new Collection<WebServiceServer>();

                foreach (XmlElement item in webserviceserverXmlNodeList)
                {
                    WebServiceServer wss = new WebServiceServer();

                    wss.ID = int.Parse(item.ChildNodes[0].InnerText);
                    wss.Name = item.ChildNodes[1].InnerText;

                    webserviceServerCol.Add(wss);
                }

                appConfig.WebServiceServers = webserviceServerCol;
            }

            #endregion

            #region DATABASE Nodes

            //based on the ENVIRONMENT we retrive all database servers
            XmlNodeList databaseserverXmlNodeList = xmlDoc.SelectNodes("//" + appQuadri + "/" + environment + "/Servers/DatabaseServers/*");

            if (databaseserverXmlNodeList.Count != 0)
            {
                Collection<DatabaseServer> databaseServerCol = new Collection<DatabaseServer>();

                foreach (XmlElement item in databaseserverXmlNodeList)
                {
                    DatabaseServer db = new DatabaseServer();

                    db.ID = int.Parse(item.ChildNodes[0].InnerText);
                    db.Name = item.ChildNodes[1].InnerText;
                    db.InstanceName = item.ChildNodes[2].InnerText;

                    databaseServerCol.Add(db);
                }

                appConfig.DatabaseServers = databaseServerCol;
            }

            #endregion

            #region BATCH Nodes

            //based on the ENVIRONMENT we retrive all batch servers
            XmlNodeList batchserverXmlNodeList = xmlDoc.SelectNodes("//" + appQuadri + "/" + environment + "/Servers/BatchServers/*");

            if (batchserverXmlNodeList.Count != 0)
            {
                Collection<BatchServer> batchServerCol = new Collection<BatchServer>();

                foreach (XmlElement item in batchserverXmlNodeList)
                {
                    BatchServer bs = new BatchServer();

                    bs.ID = int.Parse(item.ChildNodes[0].InnerText);
                    bs.Name = item.ChildNodes[1].InnerText;

                    batchServerCol.Add(bs);
                }

                appConfig.BatchServers = batchServerCol;
            }

            #endregion

            #region REPORTING Nodes

            //based on the ENVIRONMENT we retrive all reporting servers
            XmlNodeList reportingServerXmlNodeList = xmlDoc.SelectNodes("//" + appQuadri + "/" + environment + "/Servers/ReportingServers/*");

            if (reportingServerXmlNodeList.Count != 0)
            {
                Collection<ReportingServer> reportingServerCol = new Collection<ReportingServer>();

                foreach (XmlElement item in reportingServerXmlNodeList)
                {
                    ReportingServer rs = new ReportingServer();

                    rs.ID = int.Parse(item.ChildNodes[0].InnerText);
                    rs.Name = item.ChildNodes[1].InnerText;

                    reportingServerCol.Add(rs);
                }

                appConfig.ReportingServers = reportingServerCol;
            }

            #endregion

            #region WORKFLOW Nodes

            //based on the ENVIRONMENT we retrive all workflow servers
            XmlNodeList workflowServerXmlNodeList = xmlDoc.SelectNodes("//" + appQuadri + "/" + environment + "/Servers/WorkflowServers/*");

            if (workflowServerXmlNodeList.Count != 0)
            {
                Collection<WorkflowServer> workflowServerCol = new Collection<WorkflowServer>();

                foreach (XmlElement item in workflowServerXmlNodeList)
                {
                    WorkflowServer wfs = new WorkflowServer();

                    wfs.ID = int.Parse(item.ChildNodes[0].InnerText);
                    wfs.Name = item.ChildNodes[1].InnerText;

                    workflowServerCol.Add(wfs);
                }

                appConfig.WorkflowServers = workflowServerCol;
            }

            #endregion


            return appConfig;
        }

        /// <summary>
        /// Return the path for the web.config files 
        /// </summary>
        /// <param name="appConfig">The app config.</param>
        /// <returns></returns>
        public static Collection<String> GetApplicationWebConfig(ApplicationConfigBE appConfig)
        {
            Collection<String> pathCol = new Collection<string>();

            //loop for web servers
            if (appConfig.WebServers != null)
            {
                foreach (WebServer item in appConfig.WebServers)
                {
                    pathCol.Add("\\\\" + item.Name + appConfig.WEBPhysicalLocation);
                }
            }
            
            //loop for web service servers
            if (appConfig.WebServiceServers != null)
            {
                foreach (WebServiceServer item in appConfig.WebServiceServers)
                {
                    pathCol.Add("\\\\" + item.Name + appConfig.WEBPhysicalLocation);
                }
            }

            return pathCol;
        }

        public static String GetApplicationDatabase(String appQuadri, String environment)
        {
            return null;
        }

        /// <summary>
        /// Gets the web config full environment code.
        /// </summary>
        /// <param name="envCode">The env code.</param>
        /// <returns></returns>
        public static String GetWebConfigFullEnvironmentCode(String envCode)
        {
            if (envCode == "INT")
            {
                return "Integration";
            }
            else if (envCode == "UAT")
            {
                return "Uat";
            }
            else if (envCode == "STA")
            {
                return "Staging";
            }
            else
            {
                return "Production";
            }
        }
    }
}
