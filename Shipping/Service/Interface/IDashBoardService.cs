using Shipping.ViewModels.Account;
using Shipping.ViewModels.DashBoard;
using Shipping.ViewModels.Orders;
using System.Collections.Generic;

namespace Shipping.Service.Interface
{
    public interface IDashBoardService
    {
        BadgesViewModel GetBadges();
        IEnumerable<UserViewModel> GetUsers();
        IEnumerable<OrderViewModel> GetOrders();
    }
}
