using System;

namespace NeXT.DMT.Entities.Servers
{
    [Serializable]
    public class WorkflowServer
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// The workflow server name
        /// </summary>
        public String Name { get; set; }
    }
}
