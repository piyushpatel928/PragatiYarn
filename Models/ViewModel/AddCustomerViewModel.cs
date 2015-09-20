using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PragatiYarn.Models.ViewModel
{
    public class AddCustomerViewModel
    {
        public int customerId
        {
            get;
            set;
        }
        [Required(ErrorMessage = "Customer SurName is required.")]
        public string CustomerSurName
        {
            get;
            set;
        }
        [Required(ErrorMessage = "Customer Name is required.")]
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
        [Required(ErrorMessage = "Customer Address is required.")]
        public string Address
        {
            get;
            set;
        }
        [RegularExpression("^[0-9\\\\]+$", ErrorMessage = "Please Enter Valid Mobile No."), Required(ErrorMessage = "Mobile No is required."), StringLength(10, ErrorMessage = "Mobile No length must be 10 digit.")]
        public string MobileNumber
        {
            get;
            set;
        }
        public string CustomerImageName
        {
            get;
            set;
        }
        public string CustomerImagePath
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