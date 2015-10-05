using System;
using NeXT.DMT.Utilities;

namespace NeXT.DMT.Entities
{
    [Serializable]
    public class BLDocumentationBE
    {
        /// <summary>
        /// The ID of the deliverable
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// What are we delivering. Ex. Test Script
        /// </summary>
        public String DocName { get; set; }

        /// <summary>
        /// Which version. Ex. 2.1.0
        /// Should be 3 digits
        /// </summary>
        public String Version { get; set; }

        /// <summary>
        /// Location of the deliverable. Eroom or Shared folder
        /// </summary>
        public String Location { get; set; }

        /// <summary>
        /// Any remark
        /// </summary>
        public String Remark { get; set; }

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
