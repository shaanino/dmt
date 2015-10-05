using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace NeXT.DMT.Entities
{
    [DataContract]
    [Serializable]
    public class BLInformationBE
    {
        /// <summary>
        /// The application object
        /// </summary>
        [DataMember]
        public ApplicationBE App { get; set; }

        /// <summary>
        /// The delivery IFT number Ex. 587
        /// </summary>
        [DataMember]
        public int IFT { get; set; }

        /// <summary>
        /// By whom the BL was prepared
        /// </summary>
        [DataMember]
        public String PreparedBy { get; set; }

        /// <summary>
        /// The date the BL was prepared
        /// </summary>
        [DataMember]
        public DateTime PreparedDate { get; set; }

        /// <summary>
        /// Who will be approving the BL
        /// </summary>
        [DataMember]
        public String ApprovedBy { get; set; }

        /// <summary>
        /// The date the BL will be approved
        /// </summary>
        [DataMember]
        public DateTime ApprovedDate { get; set; }

        /// <summary>
        /// QC or Change Number
        /// </summary>
        [DataMember]
        public String Reference { get; set; }

        /// <summary>
        /// Short description of the QC or Change
        /// </summary>
        [DataMember]
        public String ReferenceDescription { get; set; }

        /// <summary>
        /// Softwares collection
        /// </summary>
        [DataMember]
        public Collection<BLDeliverableBE> BLSoftwareDeliverableCol { get; set; }

        /// <summary>
        /// Documents collection
        /// </summary>
        [DataMember]
        public Collection<BLDocumentationBE> BLDocumentDeliverableCol { get; set; }
    }
}
