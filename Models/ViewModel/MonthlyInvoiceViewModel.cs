using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PragatiYarn.Models.ViewModel
{
    public class MonthlyInvoiceViewModel
    {
        public int BillId
        {
            get;
            set;
        }
        public string CustomerName
        {
            get;
            set;
        }
        public string CustomerFirmName
        {
            get;
            set;
        }
        public string FromDate
        {
            get;
            set;
        }
        public string ToDate
        {
            get;
            set;
        }
        public float TotalAmount
        {
            get;
            set;
        }
        public List<ChalansViewModel> lstChalans
        {
            get;
            set;
        }
        public List<MonthlyInvoice_Down_ViewModel> lst_totaldownItems { get; set; }
        public List<MonthlyInvoice_Down_ReturnViewModel> lst_totalReturndownItems { get; set; }
        
    }
}