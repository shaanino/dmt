using System;
using System.Runtime.Serialization;
using NeXT.DMT.Utilities;
using System.Collections.ObjectModel;

namespace NeXT.DMT.Entities
{
    [Serializable]
    public class ApplicationBE
    {
        /// <summary>
        /// The ID of the application
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// The application full name EX: Predict!
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// The application quadri code EX: PRED
        /// </summary>
        public String Quadri { get; set; }

        /// <summary>
        /// Folder name where documents will be outputted EX: PRED_(MASP)
        /// </summary>
        public String FolderName { get; set; }

        /// <summary>
        /// Deliverables for the application only
        /// </summary>
        public Collection<String> Deliverables { get; set; }

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
