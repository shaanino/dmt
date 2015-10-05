using System;

namespace NeXT.DMT.Entities.Servers
{
    [Serializable]
    public class BatchServer
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Batch server name
        /// </summary>
        public String Name { get; set; }
    }
}
