using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Visport_Webservice.Handlers
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "ServiceHandlerSoap", Namespace = "http://tempuri.org/")]
    public partial class ServiceHandler : System.Web.Services.Protocols.SoapHttpClientProtocol
    {

        private System.Threading.SendOrPostCallback SyncSubscriptionDataOperationCompleted;

        /// <remarks/>
        public ServiceHandler()
        {
            this.Url = "http://localhost:8773/ServiceHandlers/ServiceHandler.asmx";
        }

        public ServiceHandler(string HandlerEndpoint)
        {
            this.Url = HandlerEndpoint;
        }

        /// <remarks/>
        public event SyncSubscriptionDataCompletedEventHandler SyncSubscriptionDataCompleted;

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/SyncSubscriptionData", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string SyncSubscriptionData(string ShortCode, string CommandCode, string UserID, string Message, string RequestID, string ServiceID, string RefID, string UpdateType, string UpdateDescription)
        {
            object[] results = this.Invoke("SyncSubscriptionData", new object[] {
                    ShortCode,
                    CommandCode,
                    UserID,
                    Message,
                    RequestID,
                    ServiceID,
                    RefID,
                    UpdateType,
                    UpdateDescription});
            return ((string)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginSyncSubscriptionData(string ShortCode, string CommandCode, string UserID, string Message, string RequestID, string ServiceID, string RefID, string UpdateType, string UpdateDescription, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("SyncSubscriptionData", new object[] {
                    ShortCode,
                    CommandCode,
                    UserID,
                    Message,
                    RequestID,
                    ServiceID,
                    RefID,
                    UpdateType,
                    UpdateDescription}, callback, asyncState);
        }

        /// <remarks/>
        public string EndSyncSubscriptionData(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }

        /// <remarks/>
        public void SyncSubscriptionDataAsync(string ShortCode, string CommandCode, string UserID, string Message, string RequestID, string ServiceID, string RefID, string UpdateType, string UpdateDescription)
        {
            this.SyncSubscriptionDataAsync(ShortCode, CommandCode, UserID, Message, RequestID, ServiceID, RefID, UpdateType, UpdateDescription, null);
        }

        /// <remarks/>
        public void SyncSubscriptionDataAsync(string ShortCode, string CommandCode, string UserID, string Message, string RequestID, string ServiceID, string RefID, string UpdateType, string UpdateDescription, object userState)
        {
            if ((this.SyncSubscriptionDataOperationCompleted == null))
            {
                this.SyncSubscriptionDataOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSyncSubscriptionDataOperationCompleted);
            }
            this.InvokeAsync("SyncSubscriptionData", new object[] {
                    ShortCode,
                    CommandCode,
                    UserID,
                    Message,
                    RequestID,
                    ServiceID,
                    RefID,
                    UpdateType,
                    UpdateDescription}, this.SyncSubscriptionDataOperationCompleted, userState);
        }

        private void OnSyncSubscriptionDataOperationCompleted(object arg)
        {
            if ((this.SyncSubscriptionDataCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SyncSubscriptionDataCompleted(this, new SyncSubscriptionDataCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        public new void CancelAsync(object userState)
        {
            base.CancelAsync(userState);
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    public delegate void SyncSubscriptionDataCompletedEventHandler(object sender, SyncSubscriptionDataCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SyncSubscriptionDataCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal SyncSubscriptionDataCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
}