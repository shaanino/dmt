using System;

namespace NeXT.DMT.Entities.Servers
{
    [Serializable]
    public class WebServer
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// The webserver name
        /// </summary>
        public String Name { get; set; }
    }
}
