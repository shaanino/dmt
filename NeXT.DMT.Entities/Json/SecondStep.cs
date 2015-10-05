using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace NeXT.DMT.Entities.Json
{
    [Serializable]
    public class SecondStep
    {
        public String PreparedBy { get; set; }

        public String ApprovedBy { get; set; }

        public String DeliveryDate { get; set; }

        public String ChangeQCAndDescription { get; set; }

        public Collection<String> Documentation { get; set; }
    }
}
