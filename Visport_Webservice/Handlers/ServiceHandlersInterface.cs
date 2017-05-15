using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Visport_Webservice.Handlers
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "ServiceHandlerSoap", Namespace = "http://tempuri.org/")]
    public interface IServiceHandlerSoap
    {

        /// <remarks/>
        [System.Web.Services.WebMethodAttribute()]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/SyncSubscriptionData", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        string SyncSubscriptionData(string ShortCode, string CommandCode, string UserID, string Message, string RequestID, string ServiceID, string RefID, string UpdateType, string UpdateDescription);
    }
}