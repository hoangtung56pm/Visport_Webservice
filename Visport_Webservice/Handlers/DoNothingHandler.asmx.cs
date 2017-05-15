using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Visport_Webservice.Handlers
{
    /// <summary>
    /// Summary description for DoNothingHandler
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class DoNothingHandler : System.Web.Services.WebService, IServiceHandlerSoap
    {

        public string SyncSubscriptionData(string ShortCode, string CommandCode, string UserID, string Message, string RequestID, string ServiceID, string RefID, string UpdateType, string UpdateDescription)
        {
            try
            {
                return "1|OK";
            }
            catch (Exception ex)
            {
                return "0|" + ex.Message;
            }
        }
    }
}
