using System;
using System.Collections.ObjectModel;

namespace NeXT.DMT.Entities.Json
{
    [Serializable]
    public class FirstStep
    {
        public String ApplicationQuadri { get; set; }

        public String ITFNumber { get; set; }

        public String NewVersion { get; set; }

        public String OldVersion { get; set; }

        public String DeltaOrFull { get; set; }

        public String CorrectOrEvol { get; set; }

        public Collection<String> Environments { get; set; }

        public Collection<String> Deliverables { get; set; }

        public Collection<String> SVNLinkAndVersion { get; set; }

    }
}
