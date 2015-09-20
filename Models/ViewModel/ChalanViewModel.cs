using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PragatiYarn.Models.ViewModel
{
    public class ChalanViewModel
    {
        public int ChalanId
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
        public string Date
        {
            get;
            set;
        }
        public float TotalAmount
        {
            get;
            set;
        }
        public int TotalPiece
        {
            get;
            set;
        }
        public List<ItemsViewModel> Items
        {
            get;
            set;
        }
    }
}