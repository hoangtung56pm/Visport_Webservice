using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Visport_Webservice.Library.Data
{
    public class Visport_MO
    {
        #region Data Members

        int _iD;
        string _user_ID;
        string _request_ID;
        string _service_ID;
        string _command_Code;
        string _message;
        string _partner_ID;
        int _serviceType;
        int _serviceId;
        string _channel;

        #endregion

        #region Properties

        public int ID
        {
            get { return _iD; }
            set
            {
                _iD = value;
            }
        }

        public string User_ID
        {
            get { return _user_ID; }
            set
            {
                _user_ID = value;
            }
        }

        public string Request_ID
        {
            get { return _request_ID; }
            set
            {
                _request_ID = value;
            }
        }

        public string Service_ID
        {
            get { return _service_ID; }
            set
            {
                _service_ID = value;
            }
        }

        public string Command_Code
        {
            get { return _command_Code; }
            set
            {
                _command_Code = value;
            }
        }

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
            }
        }

        public string Partner_ID
        {
            get { return _partner_ID; }
            set
            {
                _partner_ID = value;
            }
        }

        public int ServiceType
        {
            get { return _serviceType; }
            set
            {
                _serviceType = value;
            }
        }

        public int ServiceId
        {
            get { return _serviceId; }
            set
            {
                _serviceId = value;
            }
        }

        public string Channel
        {
            get { return _channel; }
            set
            {
                _channel = value;
            }
        }

        #endregion
    }
}