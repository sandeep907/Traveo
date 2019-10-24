using Shipping.Utilities;
using System.Collections.Generic;

namespace Shipping.ViewModels.Account
{
    public class LoginRegisterViewModel
    {
        public LoginRegisterViewModel()
        {
            login = new LoginViewModel();
            user = new UserViewModel();
        }
        public LoginViewModel login { get; set; }
        public UserViewModel user { get; set; }
        public bool IsRegister { get; set; } = false;
    }
}