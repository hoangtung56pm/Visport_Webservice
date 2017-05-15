using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Visport_Webservice.Library.Data
{
    public class Service_Info
    {
        #region Data Members

        int _iD;
        string _service_Name;
        string _product_Name;
        string _service_ID;
        string _service_Code;
        int _service_Type;
        string _register_Syntax;
        string _right_Syntax_MT;
        string _second_Register_MT;
        string _wrong_Syntax_MT;
        string _double_Register_MT;
        string _cancel_Syntax;
        string _cancel_MT;
        string _refID;
        int _periodLength;
        int _noChargeLength;
        string _description;
        int _charging_Price;
        string _OutOf_Money_MT;
        string _rules;
        string _process_WS_Url;
        int _status;
        int _created_By;
        int _modified_By;
        DateTime _created_Date;
        DateTime _modified_Date;
        string _handlerEndpoint;
        int _partnerID;

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

        public string Service_Name
        {
            get { return _service_Name; }
            set
            {
                _service_Name = value;
            }
        }

        public string Product_Name
        {
            get { return _product_Name; }
            set
            {
                _product_Name = value;
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

        public string Service_Code
        {
            get { return _service_Code; }
            set
            {
                _service_Code = value;
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

        public string Register_Syntax
        {
            get { return _register_Syntax; }
            set
            {
                _register_Syntax = value;
            }
        }

        public string Right_Syntax_MT
        {
            get { return _right_Syntax_MT; }
            set
            {
                _right_Syntax_MT = value;
            }
        }
        public string Second_Register_MT
        {
            get { return _second_Register_MT; }
            set
            {
                _second_Register_MT = value;
            }
        }

        public string Wrong_Syntax_MT
        {
            get { return _wrong_Syntax_MT; }
            set
            {
                _wrong_Syntax_MT = value;
            }
        }

        public string Double_Register_MT
        {
            get { return _double_Register_MT; }
            set
            {
                _double_Register_MT = value;
            }
        }

        public string Cancel_Syntax
        {
            get { return _cancel_Syntax; }
            set
            {
                _cancel_Syntax = value;
            }
        }

        public string Cancel_MT
        {
            get { return _cancel_MT; }
            set
            {
                _cancel_MT = value;
            }
        }

        public string RefID
        {
            get { return _refID; }
            set { _refID = value; }
        }

        public int PeriodLength
        {
            get { return _periodLength; }
            set { _periodLength = value; }
        }

        public int NoChargeLength
        {
            get { return _noChargeLength; }
            set { _noChargeLength = value; }
        }


        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
            }
        }

        public int Charging_Price
        {
            get { return _charging_Price; }
            set
            {
                _charging_Price = value;
            }
        }


        public string OutOf_Money_MT
        {
            get { return _OutOf_Money_MT; }
            set { _OutOf_Money_MT = value; }
        }

        public string Rules
        {
            get { return _rules; }
            set
            {
                _rules = value;
            }
        }

        public string Process_WS_Url
        {
            get { return _process_WS_Url; }
            set
            {
                _process_WS_Url = value;
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

        public int Created_By
        {
            get { return _created_By; }
            set
            {
                _created_By = value;
            }
        }

        public int Modified_By
        {
            get { return _modified_By; }
            set
            {
                _modified_By = value;
            }
        }

        public DateTime Created_Date
        {
            get { return _created_Date; }
            set
            {
                _created_Date = value;
            }
        }

        public DateTime Modified_Date
        {
            get { return _modified_Date; }
            set
            {
                _modified_Date = value;
            }
        }

        public string HandlerEndpoint
        {
            get { return _handlerEndpoint; }
            set
            {
                _handlerEndpoint = value;
            }
        }

        public int PartnerID
        {
            get { return _partnerID; }
            set
            {
                _partnerID = value;
            }
        }

        #endregion
    }
}