using System.Collections.ObjectModel;
using System.ServiceModel;
using NeXT.DMT.Entities;
using System;

namespace NeXT.DMT.Services.Contracts
{
    [ServiceContract]
    public interface IReferenceService
    {
        /*
        [OperationContract]
        ApplicationConfigBE GetApplicationConfig(String appQuadri, String environment);

        [OperationContract]
        void GenerateBL(BLInformationBE blInformation);

        [OperationContract]
        void GenerateRFC(RequestForChangeBE rfc);

        [OperationContract]
        Collection<DeliverableBE> GetAllDeliverables();
         * 
        [OperationContract]
        PersonBE GetPersonDetailsByID(String personID);
        */

        [OperationContract]
        Collection<ApplicationBE> GetAllApplications();

        [OperationContract]
        ApplicationBE GetApplication(String appQuadri);

        [OperationContract]
        Collection<PersonBE> GetAllPersons();

        [OperationContract]
        void PerformTheMagic(JsonBE jsonObject);
    }
}
