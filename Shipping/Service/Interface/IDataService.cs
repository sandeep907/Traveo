using Shipping.Utilities;
using System.Collections.Generic;

namespace Shipping.Service.Interface
{
    public interface IDataService
    {
        IEnumerable<IListEntry> GetRoles();
        IEnumerable<IListEntry> GetOrdersStatus();
        IEnumerable<IListEntry> GetDistricts();
       IEnumerable<IListEntry> GetProducts();
    }
}