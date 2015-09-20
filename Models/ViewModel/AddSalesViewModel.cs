using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PragatiYarn.Models.ViewModel
{
    public class AddSalesViewModel
    {
        public int CustomerId
        {
            get;
            set;
        }
        public int UserId
        {
            get;
            set;
        }
        public List<SalesItemViewModel> lstsaleitem
        {
            get;
            set;
        }

        public List<SalesItemViewModel> lstreturnsaleitem
        {
            get;
            set;
        }
        public string Datetime { get; set; }

    }
}