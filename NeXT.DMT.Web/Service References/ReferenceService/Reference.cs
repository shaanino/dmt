﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NeXT.DMT.Web.ReferenceService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ReferenceService.IReferenceService")]
    public interface IReferenceService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IReferenceService/GetAllApplications", ReplyAction="http://tempuri.org/IReferenceService/GetAllApplicationsResponse")]
        System.Collections.ObjectModel.Collection<NeXT.DMT.Entities.ApplicationBE> GetAllApplications();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IReferenceService/GetApplication", ReplyAction="http://tempuri.org/IReferenceService/GetApplicationResponse")]
        NeXT.DMT.Entities.ApplicationBE GetApplication(string appQuadri);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IReferenceService/GetAllPersons", ReplyAction="http://tempuri.org/IReferenceService/GetAllPersonsResponse")]
        System.Collections.ObjectModel.Collection<NeXT.DMT.Entities.PersonBE> GetAllPersons();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IReferenceService/PerformTheMagic", ReplyAction="http://tempuri.org/IReferenceService/PerformTheMagicResponse")]
        void PerformTheMagic(NeXT.DMT.Entities.JsonBE jsonObject);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IReferenceServiceChannel : NeXT.DMT.Web.ReferenceService.IReferenceService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ReferenceServiceClient : System.ServiceModel.ClientBase<NeXT.DMT.Web.ReferenceService.IReferenceService>, NeXT.DMT.Web.ReferenceService.IReferenceService {
        
        public ReferenceServiceClient() {
        }
        
        public ReferenceServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ReferenceServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ReferenceServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ReferenceServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Collections.ObjectModel.Collection<NeXT.DMT.Entities.ApplicationBE> GetAllApplications() {
            return base.Channel.GetAllApplications();
        }
        
        public NeXT.DMT.Entities.ApplicationBE GetApplication(string appQuadri) {
            return base.Channel.GetApplication(appQuadri);
        }
        
        public System.Collections.ObjectModel.Collection<NeXT.DMT.Entities.PersonBE> GetAllPersons() {
            return base.Channel.GetAllPersons();
        }
        
        public void PerformTheMagic(NeXT.DMT.Entities.JsonBE jsonObject) {
            base.Channel.PerformTheMagic(jsonObject);
        }
    }
}
