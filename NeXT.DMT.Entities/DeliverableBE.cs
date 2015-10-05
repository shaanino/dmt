using System;
using NeXT.DMT.Utilities;

namespace NeXT.DMT.Entities
{
    [Serializable]
    public class DeliverableBE
    {
        /// <summary>
        /// The ID of the deliverable
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// The deliverable name EX: Web Service
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// The deliverable code EX: AS
        /// </summary>
        public String Code { get; set; }

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
