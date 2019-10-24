using Shipping.Utilities;
using System.Linq;

namespace Shipping.Repository.Interface
{
    public interface IOrderRepository
    {
        IQueryable<OrdersView> Getorders();
        OrdersView GetOrderById(int id);
        OrdersView GetOrderStatusById(int id, int userId);
        OrdersView GetOrderStatusByConsigneeNo(string consigneeNo);
        OrdersView GetOrderById(int id, int userId);
        ProcResult AddOrder(Order order, Remark remark);
        ProcResult UpdateOrder(Order order, Remark remark);
        ProcResult DeleteOrder(int id);
    }
}