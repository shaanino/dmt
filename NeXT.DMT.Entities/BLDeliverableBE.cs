using System;
using NeXT.DMT.Utilities;

namespace NeXT.DMT.Entities
{
    [Serializable]
    public class BLDeliverableBE
    {
        /// <summary>
        /// The ID of the deliverable
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// What are we delivering. Ex. WEB
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// Which version. Ex. 2.1.0.0 
        /// Should be 4 digits
        /// </summary>
        public String Version { get; set; }

        /// <summary>
        /// Location of the deliverable. Eroom or Shared folder
        /// </summary>
        public String Location { get; set; }

        /// <summary>
        /// Location on SVN
        /// </summary>
        public String Etiquette { get; set; }

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
