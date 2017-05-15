using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Visport_Webservice.Library;
using Visport_Webservice.Library.Data;
using Visport_Webservice.WS_GeneralContent;
using static Visport_Webservice.Library.Common;

namespace Visport_Webservice.Handlers
{
    /// <summary>
    /// Summary description for PartnerHandler
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class PartnerHandler : System.Web.Services.WebService, IServiceHandlerSoap
    {

        public static readonly ILog _logger = LogManager.GetLogger(typeof(PartnerHandler));
        public string SyncSubscriptionData(string ShortCode, string CommandCode, string UserID, string Message, string RequestID, string ServiceID, string RefID, string UpdateType, string UpdateDescription)
        {
            try
            {
                //Service_Info service = Controller.GetByID(ConvertUtility.ToInt32(ServiceID));

                int type1 = 1;
                if (UpdateType == "0")
                {
                    type1 = 3;
                }
                //G_ID_Type_Cycle
                int id = 0;
                int type = 0;
                int cycle = 0;
                try
                {
                    string[] splRefID = RefID.Split('_');
                    id = ConvertUtility.ToInt32(splRefID[1]);
                    type = ConvertUtility.ToInt32(splRefID[2]);
                    cycle = ConvertUtility.ToInt32(splRefID[3]);

                }
                catch
                {

                }
                int messagetype = 0;
                int contenttype = 0;
                WS_PartnerContent objContent = new WS_PartnerContent();
                ContentInfo[] objContentIf;

               
                objContentIf = objContent.GetContent(id, DateTime.Now.Ticks.ToString().Substring(5, 5), UserID, type1, "979", "VNM");


               
                if (ServiceID != "370" && ServiceID != "369")
                {
                    if (objContentIf != null && type1 != 3)
                    {
                        for (int i = 0; i < objContentIf.Length; i++)
                        {
                            if (i == 0)
                            {
                                messagetype = 1;
                            }
                            else
                            {
                                messagetype = 0;
                            }
                           
                            contenttype = 0;                           
                            if (!string.IsNullOrEmpty(objContentIf[i].Content))
                            {
                                Controller.SendMT(UserID, objContentIf[i].Content, ShortCode, CommandCode, 0, ConvertUtility.ToInt32(ServiceID), MESSAGE_TYPE.Charge, RequestID, 1, 1, 0, contenttype);

                            }
                            
                        }
                    }


                }               
                return "1|OK";
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
                return "0|" + ex.Message;
            }
        }
    }
}
