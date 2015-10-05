using System;

namespace NeXT.DMT.Entities.Servers
{
    [Serializable]
    public class DatabaseServer
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Database name
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// Database instance
        /// </summary>
        public String InstanceName { get; set; }
    }
}
