using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Shipping.Utilities;

namespace Shipping.ViewModels.Account
{
    public class LoginViewModel:BaseViewModel
    {
        [Required(ErrorMessage = "User name is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Passowrd is required")]
        public string UserPassword { get; set; }
        public bool RememberMe { get; set; }
    }
}