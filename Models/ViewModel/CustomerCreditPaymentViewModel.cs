using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PragatiYarn.Models.ViewModel
{
    public class CustomerCreditPaymentViewModel
    {
        public int customerId
        {
            get;
            set;
        }
        public string CustomerName
        {
            get;
            set;
        }
        public string FirmName
        {
            get;
            set;
        }
        public float Amount
        {
            get;
            set;
        }
        public int UserId
        {
            get;
            set;
        }
    }
}