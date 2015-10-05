using System;
using System.Collections.ObjectModel;

namespace NeXT.DMT.Entities
{
    [Serializable]
    public class McopBE
    {
        public Collection<String> ServersCol { get; set; }

        public bool isLongIsoDateDefault { get; set; }

        public String PhysicalPath { get; set; }

        public String DeliverablePath { get; set; }

        public String BackupPath { get; set; }

        public String FirstAction { get; set; }

        public bool isNewFirstAction { get; set; }

        public bool isOverWriteFirstAction { get; set; }

        public String SecondAction { get; set; }

        public bool isNewSecondAction { get; set; }

        public bool isOverWriteSecondAction { get; set; }
    }
}
