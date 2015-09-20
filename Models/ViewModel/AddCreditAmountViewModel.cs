using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PragatiYarn.Models.ViewModel
{
    public class AddCreditAmountViewModel
    {
        public int customerId
        {
            get;
            set;
        }
        [RegularExpression("^[0-9\\\\]+$", ErrorMessage = "Please Enter Valid Mobile No.")]
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