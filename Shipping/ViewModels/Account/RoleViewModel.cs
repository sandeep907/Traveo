using System.ComponentModel.DataAnnotations;

namespace Shipping.ViewModels.Account
{
    public class RoleViewModel
    {
        public int RoleId { get; set; }

        [Required(ErrorMessage = "Region is required")]
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
    }
}