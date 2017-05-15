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
    /// Summary description for Job_Reset_DebitS1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Job_Reset_DebitS1 : System.Web.Services.WebService, IJobExecutorSoap
    {
        private static string ConnectionString = WebConfigurationManager.ConnectionStrings["Connttnd"].ConnectionString;
        public int Execute(int jobID)
        {
            try
            {

                Update_DebitS1();
            }
            catch (Exception ex)
            {
                return 0;
            }
            return 1;
        }
        public static void Update_DebitS1()
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text,
                        String.Format("Update [Sport_Game_Hero_Registered_Users] set DebitS1 = 0  where DebitS1 = 1"));
        }
    }
}
