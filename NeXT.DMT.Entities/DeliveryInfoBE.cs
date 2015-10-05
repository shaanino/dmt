using System;
using NeXT.DMT.Utilities;

namespace NeXT.DMT.Entities
{

    [Serializable]
    public class DeliveryInfoBE
    {
        public String AppQuadri { get; set; }

        public String RootDirectoryPath { get; set; }

        public String ITFNo { get; set; }

        public String NewVersion { get; set; }

        public String OldVersion { get; set; }

        public String NextExternalDirectory { get; set; }

        //public String Version { get; set; }

        public bool isINT { get; set; }

        public bool isUAT { get; set; }
        
        public bool isSTA { get; set; }
        
        public bool isPROD { get; set; }

        public bool isDelvWEB { get; set; }
        
        public bool isDelvWS { get; set; }
        
        public bool isDelvDB { get; set; }
        
        public bool isDelvK2PROCESS { get; set; }
        
        public bool isDelvREPORT { get; set; }
        
        public bool isDelvBATCH { get; set; }

        public bool isModifWebConfig { get; set; }

        /// <summary>
        /// Gets the version being delivered.
        /// 3 digits tag or full version
        /// </summary>
        /// <param name="isNewVersion">if set to <c>true</c> [is new version].</param>
        /// <param name="isFull">if set to <c>true</c> [is full].</param>
        /// <returns></returns>
        public String GetVersion(bool isNewVersion, bool isFull)
        {
            String version = String.Empty;

            if (isNewVersion)
            {
                version = this.NewVersion;
            }
            else
            {
                version = this.OldVersion;
            }

            if (!isFull)
            {
                //3 digits tag
                version = version.Substring(0, 5);
            }

            return version;
        }

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
