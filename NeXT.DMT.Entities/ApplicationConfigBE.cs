using System;
using System.Collections.ObjectModel;
using NeXT.DMT.Entities.Servers;
using NeXT.DMT.Utilities;

namespace NeXT.DMT.Entities
{
    [Serializable]
    public class ApplicationConfigBE
    {
        /// <summary>
        /// The Environment String which is used in the app setting
        /// </summary>
        public String AppSettingEnvironment { get; set; }

        /// <summary>
        /// Url for testing application
        /// </summary>
        public String AppTestUrl { get; set; }

        /// <summary>
        /// Root directory of the WEB server
        /// </summary>
        public String WEBPhysicalLocation { get; set; }

        /// <summary>
        /// Root directory of the WS server
        /// </summary>
        public String WSPhysicalLocation { get; set; }

        /// <summary>
        /// Root directory of the K2 WS server
        /// </summary>
        public String K2WSPhysicalLocation { get; set; }

        /// <summary>
        /// Web Servers
        /// </summary>
        public Collection<WebServer> WebServers { get; set; }

        /// <summary>
        /// Web Service Servers
        /// </summary>
        public Collection<WebServiceServer> WebServiceServers { get; set; }

        /// <summary>
        /// Database Servers
        /// </summary>
        public Collection<DatabaseServer> DatabaseServers { get; set; }

        /// <summary>
        /// Batch Servers
        /// </summary>
        public Collection<BatchServer> BatchServers { get; set; }

        /// <summary>
        /// Reporting Servers
        /// </summary>
        public Collection<ReportingServer> ReportingServers { get; set; }

        /// <summary>
        /// Workflow Servers
        /// </summary>
        public Collection<WorkflowServer> WorkflowServers { get; set; }

        /// <summary>
        /// Converts the value of this instance to its equivalent string representation.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return CommonFunction.ToString(this);
        }
    }
}
