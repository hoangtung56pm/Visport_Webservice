using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Visport_Webservice.Jobs
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "JobExecutorSoap", Namespace = "http://tempuri.org/")]
    public interface IJobExecutorSoap
    {

        /// <remarks/>
        [System.Web.Services.WebMethodAttribute()]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/Execute", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        int Execute(int jobID);
    }
}