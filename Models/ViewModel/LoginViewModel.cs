using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PragatiYarn.Models.ViewModel
{
    public class LoginViewModel
    {
        [Display(Name = "UserName"), RegularExpression("^[A-Za-z \\\\]+$", ErrorMessage = "Please Enter Valid User Name."), Required(ErrorMessage = "UserName is required.")]
        public string Username
        {
            get;
            set;
        }
        [Display(Name = "Password"), Required(ErrorMessage = "Password is required.")]
        public string Password
        {
            get;
            set;
        }
    }
}