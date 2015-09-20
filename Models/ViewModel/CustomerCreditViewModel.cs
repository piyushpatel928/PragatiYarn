using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PragatiYarn.Models.ViewModel
{
    public class CustomerCreditViewModel
    {
        public float TotalAmount { get; set; }
        public List<AllCustomerCreditViewModel> lstaccvm { get; set; }
        public string DateTime
        {
            get;
            set;
        }
    }
}