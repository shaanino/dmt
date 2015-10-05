using System;
using NeXT.DMT.Entities.Json;

namespace NeXT.DMT.Entities
{
    [Serializable]
    public class JsonBE
    {
        public FirstStep Step1 { get; set; }

        public SecondStep Step2 { get; set; }

        public ThirdStep Step3 { get; set; }
    }
}
