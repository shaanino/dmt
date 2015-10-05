using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NeXT.DMT.Entities.Json
{
    [Serializable]
    public class ThirdStep
    {
        public String ISEntity { get; set; }

        public String SubmittedBy { get; set; }

        public String SubmittedDate { get; set; }

        public String QualificationOfRequest { get; set; }

        public String Priority { get; set; }

        public String RegulatedEnvironment { get; set; }

        public String ExpectedDate { get; set; }

        public String ExpectedStartTime { get; set; }

        public String ExpectedDuration { get; set; }

        public String ChangeReason { get; set; }
    }
}
