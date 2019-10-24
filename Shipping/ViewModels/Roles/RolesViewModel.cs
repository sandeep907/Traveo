using Shipping.Repository;
using Shipping.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shipping.ViewModels.Roles
{
    public class RolesViewModel : BaseViewModel
    {
        public int RoleId { get; set; }
        [Required(ErrorMessage ="Role Name Required")]
        public string RoleName { get; set; }
        [Required(ErrorMessage = "Role Description Required")]
        public string RoleDescription { get; set; }
        public Nullable<int> EID { get; set; }
        public Nullable<System.DateTime> EIDDate { get; set; }
        public Nullable<int> UID { get; set; }
        public Nullable<System.DateTime> UIDDate { get; set; }

    }
    public class RolesSearchViewModel
    {
        public Pager Pager { get; set; }
        public int TotalCount { get; set; }
        public IEnumerable<RolesViewModel> LstRoles { get; set; }
    }
}