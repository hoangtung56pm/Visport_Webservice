using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace Visport_Webservice.Library
{
    public class Common
    {
        public static string ConnectionString = WebConfigurationManager.ConnectionStrings["Connttnd"].ConnectionString;
        public static string GetSetting(string key)
        {
            return WebConfigurationManager.AppSettings[key];
        }
        public struct PROCESSMO_RESULT
        {
            public const int UNKNOWN = -3;
            public const int EXCEPTION = -2;
            public const int WRONGSYNTAX = -1;
            public const int REGISTER_SUCCESSFULLY = 0;
            public const int DOUBLE_REGISTER = 1;
            public const int CANCEL_SUCCESSFULLY = 2;
            public const int NOT_EXISTS = 3;
            public const int SYSTEM_BUSY = 4;
            public const int BLACK_LIST = 5;
        }
        public enum SERVICE_STATUS
        {
            INACTIVE,
            ACTIVE,
            STARTING,
            RUNNING,
            DELETING,
            DELETED,
        }
        public struct MESSAGE_TYPE
        {
            public const int NoCharge = 0;
            public const int Charge = 1;
            public const int Refund = 2;
        }
        public enum USER_STATUS
        {
            INACTIVE,
            ACTIVE
        }
        public struct CONTENT_TYPE
        {
            public const int Text = 0;
            public const int RingTone = 1;
            public const int Logo = 2;
            public const int Binary = 3;
            public const int PictureMessage = 4;
            //public const int Wappush = 8;
            public const int Wappush = 0; //Edit thanh Text
        }
        public static string Normalize(string _message)
        {
            String strTmp = _message.Trim();
            strTmp = strTmp.Replace('/', ' ');
            strTmp = strTmp.Replace(',', ' ');
            strTmp = strTmp.Replace('<', ' ');
            strTmp = strTmp.Replace('>', ' ');
            strTmp = strTmp.Replace('[', ' ');
            strTmp = strTmp.Replace(']', ' ');
            strTmp = strTmp.Replace('\r', ' ');
            strTmp = strTmp.Replace('\n', ' ');

            String strResult = "";
            for (int i = 0; i < strTmp.Length; i++)
            {
                // char ch = strTmp.charAt(i);
                char ch = strTmp[i];
                if (ch == ' ')
                {
                    for (int j = i; j < strTmp.Length; j++)
                    {
                        //char ch2 = strTmp.charAt(j);
                        char ch2 = strTmp[j];
                        if (ch2 != ' ')
                        {
                            i = j;
                            strResult = strResult + ' ' + ch2;
                            break;
                        }
                    }

                }
                else
                {
                    strResult = strResult + ch;
                }
            }
            return strResult;
        }
        public static string GetNormalPhonenumber(string userId)
        {
            string retVal = userId;

            if (retVal.StartsWith("+"))
                retVal = retVal.Replace("+", string.Empty);
            if (retVal.StartsWith("0"))
            {
                retVal = "84" + retVal.Remove(0, 1);
            }

            return retVal;
        }
        public static bool IsRightSyntax(string template, string input)
        {
            string[] templateItems = template.Split('|');

            foreach (string item in templateItems)
            {
                string keyword = item.Trim();

                //if (keyword != String.Empty
                //    && input.StartsWith(keyword, StringComparison.OrdinalIgnoreCase)
                //    && (input.Length == keyword.Length || input.Substring(keyword.Length).StartsWith(" ")))
                //{
                //    return true;
                //}

                if (keyword != String.Empty && keyword.ToUpper() == input.ToUpper()) return true;
            }

            return false;
        }
        public static bool filterMsisdn(string msisdn)
        {
            if (msisdn.IndexOf("000") > -1 || msisdn.IndexOf("111") > -1 || msisdn.IndexOf("222") > -1 || msisdn.IndexOf("333") > -1 || msisdn.IndexOf("444") > -1 || msisdn.IndexOf("555") > -1 || msisdn.IndexOf("666") > -1 || msisdn.IndexOf("777") > -1 || msisdn.IndexOf("888") > -1 || msisdn.IndexOf("999") > -1)
            {
                return false;
            }
            if (msisdn.IndexOf("123") > -1 || msisdn.IndexOf("234") > -1 || msisdn.IndexOf("345") > -1 || msisdn.IndexOf("456") > -1 || msisdn.IndexOf("567") > -1 || msisdn.IndexOf("678") > -1 || msisdn.IndexOf("789") > -1 || msisdn.IndexOf("012") > -1)
            {
                return false;
            }
            if (msisdn.IndexOf("987") > -1 || msisdn.IndexOf("876") > -1 || msisdn.IndexOf("765") > -1 || msisdn.IndexOf("654") > -1 || msisdn.IndexOf("543") > -1 || msisdn.IndexOf("432") > -1 || msisdn.IndexOf("321") > -1 || msisdn.IndexOf("210") > -1)
            {
                return false;
            }
            return true;
        }
    }
}