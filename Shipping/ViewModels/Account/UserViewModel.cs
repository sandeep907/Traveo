using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Shipping.Utilities;

namespace Shipping.ViewModels.Account
{
    public class UserSeachViewModel
    {
        public Pager Pager { get; set; }
        public int TotalCount { get; set; }
        public List<UserViewModel> lstUser { get; set; }
    }
    public class UserViewModel : BaseViewModel
    {
        [Required(ErrorMessage = "User name is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Passoword is required")]
        public string UserPassword { get; set; }

        [Required(ErrorMessage = "Confirm Passoword is required")]
        [Compare("UserPassword", ErrorMessage = "Password and Confirmation Password must match.")]
        public string UserConfirmPassword { get; set; }
        public bool RememberMe { get; set; }

        //[Required(ErrorMessage = "Employee is required")]
        [Required(ErrorMessage = "Email Address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string UserEmailAddress
        { get; set; }
        public Nullable<int> Count { get; set; }
        public string Flag { get; set; }
        public Nullable<System.DateTime> LastLogin { get; set; }
        public int EID { get; set; }
        public int RoleId { get; set; }
        public string ImageUrl { get; set; }
        public Nullable<int> IsLogin { get; set; }
        public IEnumerable<IListEntry> lstRoles { get; set; }
    }
}