using Shipping.Utilities;
using Shipping.ViewModels.Account;
using Shipping.ViewModels.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shipping.ViewModels.DashBoard
{
    public class DashBoardViewModel
    {
       public BadgesViewModel Badges { get; set;}
        public List<UserViewModel> UsersPending { get; set; }
        public List<OrderViewModel> OrdersList { get; set; }
        public IEnumerable<IListEntry> lstRoles { get; set; }
    }
    public class BadgesViewModel
    {
        public int TotalOrders { get; set; }
        public int FreshOrders { get; set; }
        public int InProgress { get; set; }
        public int OutForDelivery  { get; set; }
        public int Delivered { get; set; }
        public int Returned { get; set; }
    }
}



