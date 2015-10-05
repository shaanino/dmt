using System;

namespace NeXT.DMT.Entities.Servers
{
    [Serializable]
    public class ReportingServer
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Reporting server name
        /// </summary>
        public String Name { get; set; }
    }
}
