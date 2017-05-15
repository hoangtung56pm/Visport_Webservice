using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Visport_Webservice.Handlers;
using Visport_Webservice.Library;
using Visport_Webservice.Library.Data;
using static Visport_Webservice.Library.Common;

namespace Visport_Webservice
{
    /// <summary>
    /// Summary description for MOProcess
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class MOProcess : System.Web.Services.WebService
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(MOProcess));
        [WebMethod]
        public string ProcessMO(String Command_Code, String Service_ID, String User_ID, String Message, String Request_ID)
        {
            try
            {
                //if (Common.GetSetting("flagconfirm") == "1")
                //{
                //    ProcessMO_Confirm(Command_Code, Service_ID, User_ID, Message, Request_ID, "SMS");
                //}
                //else
                //{
                ProcessMO(Command_Code, Service_ID, User_ID, Message, Request_ID, "SMS");
                //}

            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }

            return "1";
        }
        public int ProcessMO(String Command_Code, String Service_ID, String User_ID, String Message, String Request_ID, String Channel)
        {
            int retval = PROCESSMO_RESULT.UNKNOWN;
            try
            {

                #region Check blacklist
                int q = Controller.CheckBlacklist(User_ID);
                if (q > 0)
                {
                    retval = PROCESSMO_RESULT.BLACK_LIST;
                    return retval;
                }
                if (!Common.filterMsisdn(User_ID))
                {
                    retval = PROCESSMO_RESULT.BLACK_LIST;
                    return retval;
                }
                #endregion
                #region Check Syntax
                string mo = Common.Normalize(Message).ToUpper();
                Command_Code = Command_Code.ToUpper();
                User_ID = Common.GetNormalPhonenumber(User_ID);
                Service_Info matchService = null;
                bool isSubscription = false;


                List<Service_Info> listService = Controller.Visport_Subscription_Services_ListAll();
                foreach (Service_Info s2Service in listService)
                {
                    if (Common.IsRightSyntax(s2Service.Register_Syntax, mo))
                    {
                        matchService = s2Service;
                        isSubscription = true;
                        break;
                    }
                    else if (Common.IsRightSyntax(s2Service.Cancel_Syntax, mo))
                    {
                        matchService = s2Service;
                        isSubscription = false;
                        break;
                    }
                }
                string mt = "";
                if (matchService == null) // => Sai cú pháp
                {
                    mt = "Tin nhan cua quy khach sai cu phap. De duoc huong dan su dung dich vu, soan tin: HD gui 979. Tran trong cam on";
                    Visport_MO moInfo = new Visport_MO();
                    moInfo.User_ID = User_ID;
                    moInfo.Request_ID = Request_ID;
                    moInfo.Service_ID = Service_ID;
                    moInfo.Command_Code = Command_Code;
                    moInfo.Message = Message.Replace("'", " ");
                    moInfo.Partner_ID = "";
                    moInfo.ServiceType = 0;
                    moInfo.ServiceId = 0;
                    moInfo.Channel = Channel;
                    int rtMO = Controller.Visport_MO_Insert(moInfo);
                    Controller.SendMT(User_ID, mt, Service_ID, Command_Code, 0, 0, MESSAGE_TYPE.Refund, Request_ID, 1, 1, 0, CONTENT_TYPE.Text);
                    logger.ErrorFormat("*** Process MO [Short_Code:{0}], [Command_Code:{1}], [User_ID:{2}], [Request_ID:{3}], [Message:{4}], [Channel:{6}]: [MTLs:{5}]"
                                , Service_ID
                                , Command_Code
                                , User_ID
                                , Request_ID
                                , Message
                                , mt
                                , Channel);

                    retval = PROCESSMO_RESULT.WRONGSYNTAX;
                }
                else
                {
                    Visport_MO moInfo = new Visport_MO();
                    moInfo.User_ID = User_ID;
                    moInfo.Request_ID = Request_ID;
                    moInfo.Service_ID = Service_ID;
                    moInfo.Command_Code = Command_Code;
                    moInfo.Message = Message.Replace("'", " ");
                    moInfo.Partner_ID = "";
                    moInfo.ServiceType = matchService.Service_Type;
                    moInfo.ServiceId = matchService.ID;
                    moInfo.Channel = Channel;
                    int rtMO = Controller.Visport_MO_Insert(moInfo);
                    ServiceHandler handler = null;
                    string handleResponse = "";
                    if (isSubscription)
                    {
                        #region Xử lý đăng ký                    
                        List<Visport_Registered_Users> listUsers = Controller.Visport_GetUser_Info(User_ID, matchService.ID);
                        List<Visport_Registered_Users> lstCheckFisrtRegis = Controller.Visport_CheckFirst_Regis(User_ID, matchService.ID);
                        if (listUsers.Count > 0)
                        {
                            mt = matchService.Double_Register_MT;
                            Controller.SendMT(User_ID, mt, Service_ID, Command_Code, matchService.Service_Type, matchService.ID, MESSAGE_TYPE.Charge, Request_ID, 1, 1, 0, CONTENT_TYPE.Text);

                            retval = PROCESSMO_RESULT.DOUBLE_REGISTER;
                        }
                        else
                        {

                            handler = new ServiceHandler(matchService.HandlerEndpoint);
                            try
                            {
                                handleResponse = handler.SyncSubscriptionData(Service_ID, Command_Code, User_ID, Message, Request_ID, matchService.ID.ToString(), matchService.RefID, "1", "Subscription");
                                //handleResponse = "1";
                            }
                            catch (Exception ex)
                            {
                                handleResponse = "0|" + ex.Message;
                            }
                            if (handleResponse.StartsWith("1|"))
                            {
                                if (lstCheckFisrtRegis.Count > 0)
                                {
                                    //Update Status Registered_Users
                                    Visport_Registered_Users user = new Visport_Registered_Users();
                                    user.User_ID = User_ID;
                                    user.Request_ID = Request_ID;
                                    user.Service_ID = matchService.ID;
                                    user.Short_Code = Service_ID;
                                    user.Command_Code = Command_Code;
                                    user.Service_Type = matchService.Service_Type;
                                    user.Status = Convert.ToInt32(USER_STATUS.ACTIVE);
                                    user.RegistrationChannel = Channel;
                                    user.PeriodLength = matchService.PeriodLength;
                                    user.NoChargeLength = matchService.NoChargeLength;
                                    user.ChargedFee = matchService.Charging_Price;
                                    int rtvalue = Controller.Visport_Registered_Users_Update(user);
                                    if (rtvalue > 0)
                                    {
                                        mt = matchService.Right_Syntax_MT;
                                        Controller.SendMT(User_ID, mt, Service_ID, Command_Code, matchService.Service_Type, matchService.ID, MESSAGE_TYPE.Charge, Request_ID, 1, 1, 0, CONTENT_TYPE.Text);
                                        retval = PROCESSMO_RESULT.REGISTER_SUCCESSFULLY;
                                    }
                                    else
                                    {
                                        mt = "He thong dang ban. De duoc ho tro xin lien he 19001255. Cam on quy khach.";
                                        Controller.SendMT(User_ID, mt, Service_ID, Command_Code, matchService.Service_Type, matchService.ID, MESSAGE_TYPE.Refund, Request_ID, 1, 1, 0, CONTENT_TYPE.Text);
                                        retval = PROCESSMO_RESULT.SYSTEM_BUSY;
                                    }
                                }
                                else
                                {
                                    //Insert DB Registered_Users
                                    Visport_Registered_Users user = new Visport_Registered_Users();
                                    user.User_ID = User_ID;
                                    user.Request_ID = Request_ID;
                                    user.Service_ID = matchService.ID;
                                    user.Short_Code = Service_ID;
                                    user.Command_Code = Command_Code;
                                    user.Service_Type = matchService.Service_Type;
                                    user.Status = Convert.ToInt32(USER_STATUS.ACTIVE);
                                    user.RegistrationChannel = Channel;
                                    user.PeriodLength = matchService.PeriodLength;
                                    user.ChargedFee = matchService.Charging_Price;
                                    int rtvalue = Controller.Visport_Registered_Users_Insert(user);
                                    if (rtvalue > 0)
                                    {
                                        mt = matchService.Right_Syntax_MT;
                                        Controller.SendMT(User_ID, mt, Service_ID, Command_Code, matchService.Service_Type, matchService.ID, MESSAGE_TYPE.Charge, Request_ID, 1, 1, 0, CONTENT_TYPE.Text);
                                        retval = PROCESSMO_RESULT.REGISTER_SUCCESSFULLY;
                                    }
                                    else
                                    {
                                        mt = "He thong dang ban. De duoc ho tro xin lien he 19001255. Cam on quy khach.";
                                        Controller.SendMT(User_ID, mt, Service_ID, Command_Code, matchService.Service_Type, matchService.ID, MESSAGE_TYPE.Refund, Request_ID, 1, 1, 0, CONTENT_TYPE.Text);
                                        retval = PROCESSMO_RESULT.SYSTEM_BUSY;
                                    }
                                }
                            }
                            else if (handleResponse.StartsWith("0|"))
                            {
                                mt = "He thong dang ban. De duoc ho tro xin lien he 19001255. Cam on quy khach.";
                                Controller.SendMT(User_ID, mt, Service_ID, Command_Code, matchService.Service_Type, matchService.ID, MESSAGE_TYPE.Refund, Request_ID, 1, 1, 0, CONTENT_TYPE.Text);
                                retval = PROCESSMO_RESULT.SYSTEM_BUSY;
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        #region Xử lý hủy
                        List<Visport_Registered_Users> listUser = Controller.Visport_GetUser_Info(User_ID, matchService.ID);
                        if (listUser.Count > 0)
                        {
                            handler = new ServiceHandler(matchService.HandlerEndpoint);
                            try
                            {
                                handleResponse = handler.SyncSubscriptionData(Service_ID, Command_Code, User_ID, Message, Request_ID, matchService.ID.ToString(), matchService.RefID, "0", "Unsubscription");
                                //handleResponse = "1";
                            }
                            catch (Exception ex)
                            {
                                handleResponse = "0|" + ex.Message;
                            }

                            if (handleResponse.StartsWith("1|"))
                            {
                                Controller.Visport_Deactivate_Users(listUser[0].ID, "From user by SMS");
                                mt = matchService.Cancel_MT;
                                Controller.SendMT(User_ID, mt, Service_ID, Command_Code, matchService.Service_Type, matchService.ID, MESSAGE_TYPE.Charge, Request_ID, 1, 1, 0, CONTENT_TYPE.Text);
                                retval = PROCESSMO_RESULT.CANCEL_SUCCESSFULLY;
                            }
                            else if (handleResponse.StartsWith("0|"))
                            {
                                mt = "He thong dang ban. De duoc ho tro xin lien he 19001255. Cam on quy khach.";
                                Controller.SendMT(User_ID, mt, Service_ID, Command_Code, matchService.Service_Type, matchService.ID, MESSAGE_TYPE.Refund, Request_ID, 1, 1, 0, CONTENT_TYPE.Text);
                                retval = PROCESSMO_RESULT.SYSTEM_BUSY;
                            }
                        }
                        else
                        {
                            mt = "Ban khong dang ki dich vu nay.De duoc ho tro xin lien he 19001255. Cam on quy khach!";
                            Controller.SendMT(User_ID, mt, Service_ID, Command_Code, matchService.Service_Type, matchService.ID, MESSAGE_TYPE.Charge, Request_ID, 1, 1, 0, CONTENT_TYPE.Text);
                            retval = PROCESSMO_RESULT.NOT_EXISTS;
                        }
                        #endregion
                    }

                    logger.InfoFormat("*** Visport Process MO [Short_Code:{0}], [Command_Code:{1}], [User_ID:{2}], [Request_ID:{3}], [Message:{4}], [Channel:{8}]: [HandlerEndpoint:{5}], [HandlerResponse:{6}], [MTLs:{7}]"
                                       , Service_ID
                                       , Command_Code
                                       , User_ID
                                       , Request_ID
                                       , Message
                                       , matchService.HandlerEndpoint
                                       , handleResponse
                                       , mt
                                       , Channel);
                }
                
                #endregion
            }
            catch (Exception ex)
            {
                retval = PROCESSMO_RESULT.EXCEPTION;

                string mt = "He thong dang ban. Quy khach vui long quay lai sau it phut.";                
                Controller.SendMT(User_ID, mt, Service_ID, Command_Code, 0, 0, MESSAGE_TYPE.Refund, Request_ID, 1, 1, 0, CONTENT_TYPE.Text);
                logger.ErrorFormat(String.Format("*** Error Process MO [Short_Code:{0}], [Command_Code:{1}], [User_ID:{2}], [Request_ID:{3}], [Message:{4}], [Channel:{5}]: [Exception:{6}]"
                                        , Service_ID
                                        , Command_Code
                                        , User_ID
                                        , Request_ID
                                        , Message
                                        , Channel
                                        , ex));
            }
            return retval;
        }
    }
}
