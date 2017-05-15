using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using Visport_Webservice.Library;
using Visport_Webservice.Library.Data;
using static Visport_Webservice.Library.Common;

namespace Visport_Webservice.Handlers
{
    /// <summary>
    /// Summary description for AdvertisementHandler
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class AdvertisementHandler : System.Web.Services.WebService, IServiceHandlerSoap
    {

        log4net.ILog _log = log4net.LogManager.GetLogger(typeof(AdvertisementHandler));

        public string SyncSubscriptionData(string ShortCode, string CommandCode, string UserID, string Message, string RequestID, string ServiceID, string RefID, string UpdateType, string UpdateDescription)
        {
            try
            {
                if (Message.StartsWith("HD", StringComparison.OrdinalIgnoreCase)) // HD|HDSD
                {
                    Service_Info service = Controller.Visport_Subscription_Services_GetByID(ConvertUtility.ToInt32(ServiceID));
                    string mt = service.Right_Syntax_MT.Replace("Shortcode", ShortCode);
                    Controller.SendMT(UserID, mt, ShortCode, CommandCode, service.Service_Type, service.ID, MESSAGE_TYPE.NoCharge, RequestID, 1, 1, 0, CONTENT_TYPE.Text);
                }
                else if (Message.StartsWith("KT", StringComparison.OrdinalIgnoreCase)) // KTDV|KT DV
                {
                    DataTable dt = SqlHelper.ExecuteDataset(Common.ConnectionString, "Visport_GetRegisteredServices", UserID).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        string srvInfoFormat = "Quy khach dang su dung dich vu {0} tren dau so 979. Phi dich vu: {1} dong/{2} ngay. Quy khach da dang ky dich vu vao ngay {3} qua {4}. De huy dich vu, vui long soan: {5} gui 979.  De biet them ve cac dich vu khac, soan tin: HDSD gui 949 .Tran trong cam on.";
                        string srvInfo = "";
                        foreach (DataRow dr in dt.Rows)
                        {
                            srvInfo = String.Format(srvInfoFormat, dr["Product_Name"], dr["Charging_Price"], dr["PeriodLength"], dr["RegisteredTime"], dr["Registration_Channel"], dr["Cancel_Syntax"]);
                            Controller.SendMT(UserID, srvInfo, ShortCode, CommandCode, 1001, 1, MESSAGE_TYPE.NoCharge, RequestID, 1, 1, 0, CONTENT_TYPE.Text);
                        }
                    }
                    else
                    {
                        string mt = "Quy khach khong su dung dich vu nao tren dau so nay. Tran trong cam on";
                        Controller.SendMT(UserID, mt, ShortCode, CommandCode, 1001, 1, MESSAGE_TYPE.NoCharge, RequestID, 1, 1, 0, CONTENT_TYPE.Text);
                    }
                }
                else if (Message.StartsWith("HUY", StringComparison.OrdinalIgnoreCase)) // HUY TBDV
                {
                    DataTable dt = SqlHelper.ExecuteDataset(Common.ConnectionString, "Visport_GetRegisteredServices", UserID).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            Controller.Visport_Deactivate_Users(ConvertUtility.ToInt32(dr["ID"]), "HUY TBDV");

                        }

                        string mt = "Quy khach da huy thanh cong toan bo dich vu da dang ky tren dau so 979. Tran trong cam on";
                        Controller.SendMT(UserID, mt, ShortCode, CommandCode, 1001, 1, MESSAGE_TYPE.NoCharge, RequestID, 1, 1, 0, CONTENT_TYPE.Text);
                    }
                    else
                    {
                        string mt = "Quy khach khong su dung dich vu nao tren dau so nay. Tran trong cam on";
                        Controller.SendMT(UserID, mt, ShortCode, CommandCode, 1001, 1, MESSAGE_TYPE.NoCharge, RequestID, 1, 1, 0, CONTENT_TYPE.Text);
                    }
                }

                return "-1|User Guide";
            }
            catch (Exception ex)
            {
                return "0|" + ex.Message;
            }
        }
    }
}