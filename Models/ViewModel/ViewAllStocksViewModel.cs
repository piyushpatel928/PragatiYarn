using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PragatiYarn.Models.ViewModel
{
    public class ViewAllStocksViewModel
    {
        public string MaterialCode
        {
            get;
            set;
        }
        public string MaterialImage
        {
            get;
            set;
        }
        public string Stock
        {
            get;
            set;
        }
        public string LastAddedStock
        {
            get;
            set;
        }
        public string UpdatedDateTime
        {
            get;
            set;
        }
    }
}