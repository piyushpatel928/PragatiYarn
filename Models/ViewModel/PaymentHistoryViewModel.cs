﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PragatiYarn.Models.ViewModel
{
    public class PaymentHistoryViewModel
    {
        public int PaidId
        {
            get;
            set;
        }
        public float Amount
        {
            get;
            set;
        }
        public string DateTime
        {
            get;
            set;
        }
    }
}