using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Visport_Webservice.Library.Data
{
    public class Visport_Registered_Users
    {
        #region Data Members

        int _iD;
        string _user_ID;
        string _request_ID;
        int _service_ID;
        string _short_Code;
        string _command_Code;
        string _reference_ID;

        //int periodLength;
        //int noChargeLength;

        int _service_Type;
        int _charging_Count;
        int _failed_Charging_Times;
        int _status;
        string _registrationChannel;

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

        public int Service_ID
        {
            get { return _service_ID; }
            set
            {
                _service_ID = value;
            }
        }

        public string Short_Code
        {
            get { return _short_Code; }
            set
            {
                _short_Code = value;
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

        public string Reference_ID
        {
            get { return _reference_ID; }
            set
            {
                _reference_ID = value;
            }
        }

        public int Service_Type
        {
            get { return _service_Type; }
            set
            {
                _service_Type = value;
            }
        }

        public int Charging_Count
        {
            get { return _charging_Count; }
            set
            {
                _charging_Count = value;
            }
        }

        public int Failed_Charging_Times
        {
            get { return _failed_Charging_Times; }
            set
            {
                _failed_Charging_Times = value;
            }
        }

        public int Status
        {
            get { return _status; }
            set
            {
                _status = value;
            }
        }

        public string RegistrationChannel
        {
            get { return _registrationChannel; }
            set { _registrationChannel = value; }
        }

        public int PeriodLength { get; set; }

        public int NoChargeLength { get; set; }

        public DateTime ExpiredTime { get; set; }

        public int ChargedFee { get; set; }

        public int CountTo_Cancel { get; set; }
        #endregion
    }
}