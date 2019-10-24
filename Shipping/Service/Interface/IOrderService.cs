using Shipping.Utilities;
using Shipping.ViewModels.Orders;
using System.Collections.Generic;

namespace Shipping.Service.Interface
{
    public interface IOrderService
    {
        IEnumerable<OrderViewModel> GetAll(int Id, int roleId);
        IEnumerable<ExportOrderViewModel> ExportGetAll(int Id, int roleId);
        OrderViewModel GetStatusById(int id, int UserId, int roleId);
        OrderViewModel StatusByConsigneeNo(string consigneeNo, int roleId);
        IEnumerable<OrderViewModel> GetAll(string search, int Id, int roleId);
        OrderViewModel GetById(int id, int UserId, int roleId);
        OrderViewModel PrntGetById(int id);
        ProcResult Add(OrderViewModel model);
        ProcResult Update(OrderViewModel model);
        ProcResult Delete(int id);
    }
}
