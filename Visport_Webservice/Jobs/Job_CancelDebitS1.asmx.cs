using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Services;
using Visport_Webservice.Library;

namespace Visport_Webservice.Jobs
{
    /// <summary>
    /// Summary description for Job_CancelDebitS1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Job_CancelDebitS1 : System.Web.Services.WebService, IJobExecutorSoap
    {
        private static string ConnectionString = WebConfigurationManager.ConnectionStrings["Connttnd"].ConnectionString;
        public int Execute(int jobID)
        {
            try
            {

                DataTable dt = GetAll_DebitS1_Count();
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        Controller.Visport_Deactivate_Users(ConvertUtility.ToInt32(item["ID"].ToString()), "Result:26,Detail:Can not debit S1.");
                    }

                }
            }
            catch (Exception ex)
            {
                return 0;
            }
            return 1;
        }
        public static DataTable GetAll_DebitS1_Count()
        {
            DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text,
                        String.Format("select ID,User_ID from [Sport_Game_Hero_Registered_Users] where Status = 1 and Count_DebitS1 >= 5"));
            if (ds != null && ds.Tables.Count > 0)
                return ds.Tables[0];
            return null;
        }
    }
}
