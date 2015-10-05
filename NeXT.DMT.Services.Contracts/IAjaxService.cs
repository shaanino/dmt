using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using NeXT.DMT.Entities;
using System.Collections.ObjectModel;
using System.ServiceModel.Web;
using System.ServiceModel.Activation;

namespace NeXT.DMT.Services.Contracts
{
    [ServiceContract(Namespace = "")]
    public interface IAjaxService
    {
        [OperationContract ]
        [WebInvoke(Method = "POST",
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        Collection<ApplicationBE> GetAllApplications();


        [OperationContract]
        [WebInvoke(Method = "POST",
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        void GenerateBL(BLInformationBE blInformation);

        [OperationContract]
        [WebInvoke(Method = "POST",
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        void GenerateRFC(RequestForChangeBE rfc);

        [OperationContract]
        [WebInvoke(Method = "POST",
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        Collection<DeliverableBE> GetAllDeliverables();
    }
}
