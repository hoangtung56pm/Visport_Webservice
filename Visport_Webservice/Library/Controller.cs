using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using Visport_Webservice.Library.Data;
using static Visport_Webservice.Library.Common;

namespace Visport_Webservice.Library
{

    public class Controller
    {
        private static string ConnectionString = WebConfigurationManager.ConnectionStrings["Connttnd"].ConnectionString;
        public static List<Service_Info> Visport_Subscription_Services_ListAll()
        {
            string sql = String.Format("Select * from Visport_Subscription_Services where  Status={0} order by ID desc", (int)SERVICE_STATUS.ACTIVE);
            return Service_Info_GetBySql(sql);
        }
        public static Service_Info Visport_Subscription_Services_GetByID(int service_ID)
        {
            Service_Info retVal = null;
            SqlConnection dbConn = new SqlConnection(ConnectionString);

            SqlCommand dbCmd = new SqlCommand("Visport_Subscription_Services_GetByID", dbConn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@ID", service_ID);

            try
            {
                dbConn.Open();
                SqlDataReader dr = dbCmd.ExecuteReader();
                if (dr.Read())
                {
                    retVal = new Service_Info();
                    retVal.ID = ConvertUtility.ToInt32(dr["ID"]);
                    retVal.Service_Name = ConvertUtility.ToString(dr["Service_Name"]);
                    retVal.Service_ID = ConvertUtility.ToString(dr["Service_ID"]);
                    retVal.Service_Type = ConvertUtility.ToInt32(dr["Service_Type"]);
                    retVal.Service_Code = ConvertUtility.ToString(dr["Service_Code"]);
                    retVal.Register_Syntax = ConvertUtility.ToString(dr["Register_Syntax"]);
                    retVal.Right_Syntax_MT = ConvertUtility.ToString(dr["Right_Syntax_MT"]);
                    retVal.Wrong_Syntax_MT = ConvertUtility.ToString(dr["Wrong_Syntax_MT"]);
                    retVal.Double_Register_MT = ConvertUtility.ToString(dr["Double_Register_MT"]);
                    retVal.Cancel_Syntax = ConvertUtility.ToString(dr["Cancel_Syntax"]);
                    retVal.Cancel_MT = ConvertUtility.ToString(dr["Cancel_MT"]);
                    retVal.RefID = ConvertUtility.ToString(dr["RefID"]);
                    retVal.PeriodLength = ConvertUtility.ToInt32(dr["PeriodLength"]);
                    retVal.NoChargeLength = ConvertUtility.ToInt32(dr["NoChargeLength"]);                    
                    retVal.Description = ConvertUtility.ToString(dr["Description"]);
                    retVal.Charging_Price = ConvertUtility.ToInt32(dr["Charging_Price"]);                   
                    retVal.OutOf_Money_MT = ConvertUtility.ToString(dr["OutOf_Money_MT"]);
                    retVal.Rules = ConvertUtility.ToString(dr["Rules"]);                    
                    retVal.Process_WS_Url = ConvertUtility.ToString(dr["Process_WS_Url"]);
                    retVal.Status = ConvertUtility.ToInt32(dr["Status"]);
                    retVal.Created_By = ConvertUtility.ToInt32(dr["Created_By"]);
                    retVal.Created_Date = ConvertUtility.ToDateTime(dr["Created_Date"]);
                    retVal.Modified_By = ConvertUtility.ToInt32(dr["Modified_By"]); ;
                    retVal.Modified_Date = ConvertUtility.ToDateTime(dr["Modified_Date"]); ;
                    retVal.HandlerEndpoint = ConvertUtility.ToString(dr["HandlerEndpoint"]);

                }

                dr.Close();
            }
            finally
            {
                dbConn.Close();
            }
            return retVal;
        }
        public static List<Service_Info> Service_Info_GetBySql(string commandText)
        {
            List<Service_Info> list = new List<Service_Info>();

            SqlConnection dbConn = new SqlConnection(ConnectionString);
            SqlCommand sqlCommand = new SqlCommand(commandText, dbConn);
            try
            {
                dbConn.Open();
                SqlDataReader dataReader = sqlCommand.ExecuteReader();

                list = PopulateObjectsFromReader(dataReader);
                dataReader.Close();
            }
            finally
            {
                dbConn.Close();
                sqlCommand.Dispose();
            }

            return list;
        }
        private static List<Service_Info> PopulateObjectsFromReader(IDataReader dataReader)
        {
            List<Service_Info> list = new List<Service_Info>();

            while (dataReader.Read())
            {
                Service_Info service = new Service_Info();
                service.ID = ConvertUtility.ToInt32(dataReader["ID"]);
                service.Service_Name = ConvertUtility.ToString(dataReader["Service_Name"]);
                service.Service_ID = ConvertUtility.ToString(dataReader["Service_ID"]);
                service.Product_Name = ConvertUtility.ToString(dataReader["Product_Name"]);
                service.Service_Code = ConvertUtility.ToString(dataReader["Service_Code"]);
                service.Service_Type = ConvertUtility.ToInt32(dataReader["Service_Type"]);
                service.Register_Syntax = ConvertUtility.ToString(dataReader["Register_Syntax"]);
                service.Right_Syntax_MT = ConvertUtility.ToString(dataReader["Right_Syntax_MT"]);
                service.Wrong_Syntax_MT = ConvertUtility.ToString(dataReader["Wrong_Syntax_MT"]);
                service.Double_Register_MT = ConvertUtility.ToString(dataReader["Double_Register_MT"]);
                service.Cancel_Syntax = ConvertUtility.ToString(dataReader["Cancel_Syntax"]);
                service.Cancel_MT = ConvertUtility.ToString(dataReader["Cancel_MT"]);
                service.RefID = ConvertUtility.ToString(dataReader["RefID"]);
                service.PeriodLength = ConvertUtility.ToInt32(dataReader["PeriodLength"]);
                service.NoChargeLength = ConvertUtility.ToInt32(dataReader["NoChargeLength"]);
                service.Description = ConvertUtility.ToString(dataReader["Description"]);
                service.Charging_Price = ConvertUtility.ToInt32(dataReader["Charging_Price"]);
                service.OutOf_Money_MT = ConvertUtility.ToString(dataReader["OutOf_Money_MT"]);
                service.Rules = ConvertUtility.ToString(dataReader["Rules"]);
                service.Process_WS_Url = ConvertUtility.ToString(dataReader["Process_WS_Url"]);
                service.Status = ConvertUtility.ToInt32(dataReader["Status"]);
                service.Created_By = ConvertUtility.ToInt32(dataReader["Created_By"]);
                service.Modified_By = ConvertUtility.ToInt32(dataReader["Modified_By"]);
                service.Created_Date = ConvertUtility.ToDateTime(dataReader["Created_Date"]);
                service.Modified_Date = ConvertUtility.ToDateTime(dataReader["Modified_Date"]);
                service.HandlerEndpoint = ConvertUtility.ToString(dataReader["HandlerEndpoint"]);

                list.Add(service);
            }

            return list;
        }
        public static List<Visport_Registered_Users> Visport_GetUser_Info(string User_ID,int Service_ID)
        {
            string sql = "Select * from Sport_Game_Hero_Registered_Users where User_ID='" + User_ID + "' and ServiceID=" + Service_ID + " and Status=1 ";
            return User_Info_GetBySql(sql);
        }
        public static List<Visport_Registered_Users> Visport_CheckFirst_Regis(string User_ID, int Service_ID)
        {
            string sql = "Select * from Sport_Game_Hero_Registered_Users where User_ID='" + User_ID + "' and ServiceID=" + Service_ID + "";
            return User_Info_GetBySql(sql);
        }
        public static List<Visport_Registered_Users> User_Info_GetBySql(string commandText)
        {
            List<Visport_Registered_Users> listUsers = new List<Visport_Registered_Users>();

            SqlConnection dbConn = new SqlConnection(ConnectionString);
            SqlCommand sqlCommand = new SqlCommand(commandText, dbConn);
            try
            {
                dbConn.Open();
                SqlDataReader dataReader = sqlCommand.ExecuteReader();

                listUsers = Populate_UserFromReader(dataReader);

                dataReader.Close();
            }
            finally
            {
                dbConn.Close();
                sqlCommand.Dispose();
            }

            return listUsers;
        }
        private static List<Visport_Registered_Users> Populate_UserFromReader(IDataReader dataReader)
        {
            List<Visport_Registered_Users> listUsers = new List<Visport_Registered_Users>();

            while (dataReader.Read())
            {
                Visport_Registered_Users user = new Visport_Registered_Users();
                user.ID = ConvertUtility.ToInt32(dataReader["ID"]);
                user.User_ID = ConvertUtility.ToString(dataReader["User_ID"]);
                user.Request_ID = ConvertUtility.ToString(dataReader["Request_ID"]);
                user.Service_ID = ConvertUtility.ToInt32(dataReader["ServiceID"]);
                user.Short_Code = ConvertUtility.ToString(dataReader["Service_ID"]);
                user.Command_Code = ConvertUtility.ToString(dataReader["Command_Code"]);
                user.Reference_ID = ConvertUtility.ToString(dataReader["Reference_ID"]);
                user.Service_Type = ConvertUtility.ToInt32(dataReader["Service_Type"]);
                user.Charging_Count = ConvertUtility.ToInt32(dataReader["Charging_Count"]);
                user.Failed_Charging_Times = ConvertUtility.ToInt32(dataReader["FailedChargingTimes"]);
                user.Status = ConvertUtility.ToInt32(dataReader["Status"]);
                user.RegistrationChannel = ConvertUtility.ToString(dataReader["Registration_Channel"]);
                //user.ChargedFee = ConvertUtility.ToInt32(dataReader["ChargedFee"]);
                listUsers.Add(user);
            }

            return listUsers;
        }
        #region MO process
        public static int Visport_MO_Insert(Visport_MO _MO)
        {
            SqlConnection dbConn = new SqlConnection(ConnectionString);
            SqlCommand dbCmd = new SqlCommand("Visport_MO_Insert", dbConn);
            dbCmd.CommandType = CommandType.StoredProcedure;

            dbCmd.Parameters.AddWithValue("@User_ID", _MO.User_ID);
            dbCmd.Parameters.AddWithValue("@Request_ID", _MO.Request_ID);
            dbCmd.Parameters.AddWithValue("@Service_ID", _MO.Service_ID);
            dbCmd.Parameters.AddWithValue("@Command_Code", _MO.Command_Code);
            dbCmd.Parameters.AddWithValue("@Message", _MO.Message);
            dbCmd.Parameters.AddWithValue("@Partner_ID", _MO.Partner_ID);
            dbCmd.Parameters.AddWithValue("@ServiceType", _MO.ServiceType);
            dbCmd.Parameters.AddWithValue("@ServiceId", _MO.ServiceId);
            dbCmd.Parameters.AddWithValue("@Channel", _MO.Channel);

            dbCmd.Parameters.AddWithValue("@RETURN_VALUE", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

            try
            {
                dbConn.Open();
                dbCmd.ExecuteNonQuery();
                return (int)dbCmd.Parameters["@RETURN_VALUE"].Value;
            }
            finally
            {
                dbConn.Close();
            }
        }
        #endregion
        public static int SendMT(
                string UserId,
                string Content,
                string ShortCode,
                string CommandCode,
                int ServiceType,
                int ServiceId,
                int MessageType,
                string RequestId,
                int TotalMessage,
                int MessageIndex,
                int IsMore,
                int ContentType
            )
        {
            //------------------
            if (UserId.StartsWith("+"))
                UserId = UserId.Replace("+", string.Empty);
            if (UserId.StartsWith("0"))
            {
                UserId = "84" + UserId.Remove(0, 1);
            }

            Visport_MT_Info mtInfo = new Visport_MT_Info();
            mtInfo.User_ID = UserId;
            mtInfo.Message = Content;
            mtInfo.Short_Code = ShortCode;
            mtInfo.Command_Code = CommandCode;
            mtInfo.Message_Type = MessageType;
            mtInfo.Request_ID = RequestId;
            mtInfo.Total_Message = TotalMessage;
            mtInfo.Message_Index = MessageIndex;
            mtInfo.IsMore = IsMore;
            mtInfo.Content_Type = ContentType;
            mtInfo.MT_Price = 0;
            mtInfo.Service_Type = ServiceType;
            mtInfo.Service_ID = ServiceId;
            mtInfo.Partner_ID = "VMG";
            mtInfo.Operator = "VNM";           

            int result = Insert(mtInfo);
            return result;
        }
        public static int Insert(Visport_MT_Info mt_info)
        {
            SqlConnection dbConn = new SqlConnection(ConnectionString);
            SqlCommand dbCmd = new SqlCommand("Visport_MT_Sending_Insert", dbConn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@User_ID", mt_info.User_ID);
            dbCmd.Parameters.AddWithValue("@Message", mt_info.Message);
            dbCmd.Parameters.AddWithValue("@Short_Code", mt_info.Short_Code);
            dbCmd.Parameters.AddWithValue("@Command_Code", mt_info.Command_Code);
            dbCmd.Parameters.AddWithValue("@Message_Type", mt_info.Message_Type);
            dbCmd.Parameters.AddWithValue("@Request_ID", mt_info.Request_ID);
            dbCmd.Parameters.AddWithValue("@Total_Message", mt_info.Total_Message);
            dbCmd.Parameters.AddWithValue("@Message_Index", mt_info.Message_Index);
            dbCmd.Parameters.AddWithValue("@IsMore", mt_info.IsMore);
            dbCmd.Parameters.AddWithValue("@Content_Type", mt_info.Content_Type);
            dbCmd.Parameters.AddWithValue("@MT_Price", mt_info.MT_Price);
            dbCmd.Parameters.AddWithValue("@Service_Type", mt_info.Service_Type);
            dbCmd.Parameters.AddWithValue("@Service_ID", mt_info.Service_ID);
            dbCmd.Parameters.AddWithValue("@Partner_ID", mt_info.Partner_ID);
            dbCmd.Parameters.AddWithValue("@Operator", mt_info.Operator);            
            dbCmd.Parameters.AddWithValue("@RETURN_VALUE", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
            try
            {
                dbConn.Open();
                dbCmd.ExecuteNonQuery();
                return (int)dbCmd.Parameters["@RETURN_VALUE"].Value;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dbConn.Close();
            }
        }
        public static int Visport_Registered_Users_Insert(Visport_Registered_Users entity)
        {
            return (int)SqlHelper.ExecuteScalar(ConnectionString, "Visport_Registered_Users_Insert"
                , entity.User_ID
                , entity.Request_ID
                , entity.Service_ID
                , entity.Command_Code
                , entity.Service_Type
                , entity.Charging_Count
                , entity.Failed_Charging_Times     
                , entity.RegistrationChannel
                , entity.Status    
                , entity.PeriodLength
                 ,entity.ChargedFee      
                );            
        }
        public static int Visport_Registered_Users_Update(Visport_Registered_Users entity)
        {
            return (int)SqlHelper.ExecuteScalar(ConnectionString, "Visport_Registered_Users_Update"
                , entity.User_ID
                , entity.Request_ID
                , entity.Service_ID
                , entity.Command_Code
                , entity.Service_Type
                , entity.Charging_Count
                , entity.Failed_Charging_Times
                , entity.RegistrationChannel
                , entity.Status
                , entity.PeriodLength
                 , entity.ChargedFee
                );
        }

        public static void Visport_Deactivate_Users(int _userID, string _cancelChannel)
        {
            SqlConnection dbConn = new SqlConnection(ConnectionString);
            SqlCommand dbCmd = new SqlCommand("Visport_Deactivate_Users", dbConn);
            dbCmd.CommandType = CommandType.StoredProcedure;

            dbCmd.Parameters.AddWithValue("@ID", _userID);
            dbCmd.Parameters.AddWithValue("@Cancel_Channel", _cancelChannel);

            try
            {
                dbConn.Open();
                dbCmd.ExecuteNonQuery();
            }
            finally
            {




                dbConn.Close();
            }

        }
        public static int CheckBlacklist(string user_id)
        {
            string con = WebConfigurationManager.ConnectionStrings["Connttnd"].ConnectionString;
            SqlConnection conn = new SqlConnection(con);
            string cmdText = "Select ID from Visport_Blacklist where msisdn = '" + user_id + "'";
            SqlCommand sqlCommand = new SqlCommand(cmdText, conn);

            try
            {
                conn.Open();
                int regionid = ConvertUtility.ToInt32(sqlCommand.ExecuteScalar());
                return regionid;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
                sqlCommand.Dispose();
            }

        }
        public static int Visport_Registered_Users_Insert_Tool(Visport_Registered_Users entity)
        {
            return (int)SqlHelper.ExecuteScalar(ConnectionString, "Visport_Registered_Users_Insert_Tool"
                , entity.User_ID
                , entity.Request_ID
                , entity.Service_ID
                , entity.Command_Code
                , entity.Service_Type
                , entity.Charging_Count
                , entity.Failed_Charging_Times
                , entity.RegistrationChannel
                , entity.Status
                , entity.PeriodLength
                 , entity.ChargedFee
                 ,entity.CountTo_Cancel
                );
        }
        public static int Visport_Registered_Users_Update_Tool(Visport_Registered_Users entity)
        {
            return (int)SqlHelper.ExecuteScalar(ConnectionString, "Visport_Registered_Users_Update_Tool"
                , entity.User_ID
                , entity.Request_ID
                , entity.Service_ID
                , entity.Command_Code
                , entity.Service_Type
                , entity.Charging_Count
                , entity.Failed_Charging_Times
                , entity.RegistrationChannel
                , entity.Status
                , entity.PeriodLength
                 , entity.ChargedFee
                 , entity.CountTo_Cancel
                );
        }
    }
}