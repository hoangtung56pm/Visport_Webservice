using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Visport_Webservice.Library.Data
{
    public class Visport_MT_Info
    {
        private int _iD;
        private string _user_ID;
        private string _message;
        private string _short_Code;
        private string _command_Code;
        private int _message_Type;
        private string _request_ID;
        private int _total_Message;
        private int _message_Index;
        private int _isMore;
        private int _content_Type;
        private int _mt_Price;
        private int _service_Type;
        private int _service_ID;
        private string _partner_ID;
        private string _operator;
        

        public int ID
        {
            get { return _iD; }
            set { _iD = value; }
        }

        public string User_ID
        {
            get { return _user_ID; }
            set { _user_ID = value; }
        }

        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        public string Short_Code
        {
            get { return _short_Code; }
            set { _short_Code = value; }
        }

        public string Command_Code
        {
            get { return _command_Code; }
            set { _command_Code = value; }
        }

        public int Message_Type
        {
            get { return _message_Type; }
            set { _message_Type = value; }
        }

        public string Request_ID
        {
            get { return _request_ID; }
            set { _request_ID = value; }
        }

        public int Total_Message
        {
            get { return _total_Message; }
            set { _total_Message = value; }
        }

        public int Message_Index
        {
            get { return _message_Index; }
            set { _message_Index = value; }
        }

        public int IsMore
        {
            get { return _isMore; }
            set { _isMore = value; }
        }

        public int Content_Type
        {
            get { return _content_Type; }
            set { _content_Type = value; }
        }

        public int MT_Price
        {
            get { return _mt_Price; }
            set { _mt_Price = value; }
        }

        public int Service_Type
        {
            get { return _service_Type; }
            set { _service_Type = value; }
        }

        public int Service_ID
        {
            get { return _service_ID; }
            set { _service_ID = value; }
        }

        public string Partner_ID
        {
            get { return _partner_ID; }
            set { _partner_ID = value; }
        }

        public string Operator
        {
            get { return _operator; }
            set { _operator = value; }
        }
       
    }
}