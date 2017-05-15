using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Services;
using Visport_Webservice.Library;
using Visport_Webservice.Library.Data;
using static Visport_Webservice.Library.Common;

namespace Visport_Webservice
{
    /// <summary>
    /// Summary description for VnmVsp
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class VnmVsp : System.Web.Services.WebService
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(VnmVsp));

        private static string ConnectionString = WebConfigurationManager.ConnectionStrings["Connttnd"].ConnectionString;
        [WebMethod]
        public string RegisterService(string Shortcode, string RequestID, string Msisdn, string Commandcode, string Message)
        {
            string retVal = "-3|Unknown";

            int result = new MOProcess().ProcessMO(Commandcode, Shortcode, Msisdn, Message, RequestID, "WAP");
            switch (result)
            {
                case PROCESSMO_RESULT.WRONGSYNTAX:
                    retVal = "-1|WrongSyntax";
                    break;
                case PROCESSMO_RESULT.DOUBLE_REGISTER:
                    retVal = "0|DoubleRegister";
                    break;
                case PROCESSMO_RESULT.REGISTER_SUCCESSFULLY:
                    retVal = "1|Success";
                    break;
                case PROCESSMO_RESULT.SYSTEM_BUSY:
                    retVal = "-2|System busy";
                    break;
            }

            return retVal;
        }
        [WebMethod]
        public string SynchronizeUser(string Shortcode, string RequestID, string Msisdn, string Commandcode, string Message, int ServiceID, int SyncType, string content, int chargedDay)
        {
            string retVal = "0|Unidentified";
            try
            {
                logger.Debug("Msisdn :"+ Msisdn+ "=ServiceID="+ ServiceID);
                if (ServiceID>0)
                {
                    string id = SqlHelper.ExecuteScalar(ConnectionString, "Visport_GetByUserAndServiceId_Active", Msisdn, ServiceID).ToString();
                    List<Visport_Registered_Users> lstCheckFisrtRegis = Controller.Visport_CheckFirst_Regis(Msisdn, ServiceID);
                    int uId = ConvertUtility.ToInt32(id);

                    //  Add
                    if (SyncType == 1)
                    {
                        if (uId > 0)
                        {
                            retVal = "0|User already existed";
                        }
                        else
                        {
                            Service_Info service = Controller.Visport_Subscription_Services_GetByID(ServiceID);
                            if (service != null)
                            {
                                Visport_Registered_Users user = new Visport_Registered_Users();
                                user.User_ID = Msisdn;
                                user.Request_ID = RequestID;
                                user.Service_ID = service.ID;
                                user.Short_Code = Shortcode;
                                user.Command_Code = Commandcode;
                                user.Reference_ID = service.RefID;
                                user.Service_Type = service.Service_Type;
                                user.Charging_Count = service.NoChargeLength;

                                user.PeriodLength = service.PeriodLength;
                                user.NoChargeLength = service.NoChargeLength;

                                user.Status = 1;
                                user.RegistrationChannel = "wap";
                                user.CountTo_Cancel = service.PeriodLength + chargedDay;
                                if (lstCheckFisrtRegis.Count > 0)
                                {
                                    int rtvalue = Controller.Visport_Registered_Users_Update_Tool(user);
                                }
                                else
                                {
                                    int rtvalue = Controller.Visport_Registered_Users_Insert_Tool(user);
                                }
                                //S2_Registered_Users_DB.Insert(user);
                                //S2_Registered_Users_DB.InsertImport(user, chargedDay);

                                #region INSERT INTO MT_LOG
                                Controller.SendMT(Msisdn, content, Shortcode, Commandcode, service.Service_Type, service.ID, MESSAGE_TYPE.Charge, RequestID, 1, 1, 0, CONTENT_TYPE.Text);

                                #endregion
                            }

                            retVal = "1";
                        }
                    }
                    else if (SyncType == 0) // Delete
                    {
                        if (uId > 0)
                        {
                            Controller.Visport_Deactivate_Users(uId, "");

                            retVal = "1";
                        }
                        else
                        {
                            retVal = "0|not exists";
                        }
                    }
                }
                else
                {
                    retVal = "0|not exists";
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                retVal = "0|" + ex.Message;
            }

            return retVal;
        }
    }
}
