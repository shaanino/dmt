using System;
using NeXT.DMT.Utilities;

namespace NeXT.DMT.Entities
{
    [Serializable]
    public class RequestForChangeBE
    {
        /// <summary>
        /// IS/IT Entity EX: COS
        /// </summary>
        public String Entity { get; set; }

        /// <summary>
        /// Who submitted the RFC
        /// </summary>
        public String SubmittedBy { get; set; }

        /// <summary>
        /// The date the request was submitted
        /// </summary>
        public DateTime SubmittedDate { get; set; }

        /// <summary>
        /// Is it Major / Medium / Minor
        /// </summary>

        public String QualificationOfRequest { get; set; }
        //public EnumBE.QualificationOfRequest QualificationOfRequest { get; set; }

        /// <summary>
        /// The priority of the request [1 is the HIGHEST priority]
        /// </summary>
        public String Priority { get; set; }
        //public EnumBE.Priority Priority { get; set; }

        /// <summary>
        /// The regulated environment
        /// </summary>
        public String RegulatedEnvironment { get; set; }
        //public EnumBE.RegulatedEnvironment RegulatedEnvironment { get; set; }

        /// <summary>
        /// On which date the deployment is targetted
        /// </summary>
        public String ExpectedDate { get; set; }

        /// <summary>
        /// At what time
        /// </summary>
        public String ExpectedStartTime { get; set; }

        /// <summary>
        /// How much it will take to make the deployment
        /// </summary>
        public String ExpectedDuration { get; set; }

        /// <summary>
        /// Why we are making a deployment. EX: Deployment of Predict! 1.3.0 on UAT
        /// </summary>
        public String ChangeReason { get; set; }

        /// <summary>
        /// On which environment INT/UAT/STA/PROD
        /// </summary>
        public String Environment { get; set; }

        /// <summary>
        /// All the servers information
        /// </summary>
        public String ServerInformation { get; set; }

        /// <summary>
        /// The impact the deployment will have on each services.
        /// </summary>
        public String ImpactOnService { get; set; }

        /// <summary>
        /// Gets or sets the HPSD ticket ID.
        /// </summary>
        public String HPSDTicketID { get; set; }

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
