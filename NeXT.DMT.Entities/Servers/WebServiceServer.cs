using System;

namespace NeXT.DMT.Entities.Servers
{
    [Serializable]
    public class WebServiceServer
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// The webservice server name
        /// </summary>
        public String Name { get; set; }
    }
}
