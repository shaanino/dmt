using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NeXT.DMT.Utilities;


namespace NeXT.DMT.Entities
{
    [Serializable]
    public class PersonBE
    {

        //Important Note
        //If you don't want the application to crash and get yourself killed
        //don't touch this class
        //else the autosuggest function won't work 

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The person name.
        /// </value>
        public String name { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>
        /// The person id.
        /// </value>
        public String id { get; set; }

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
