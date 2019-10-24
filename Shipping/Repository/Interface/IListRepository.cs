using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Repository.Interface
{
    public interface IListRepository
    {
        IQueryable<MTRoleMaster> GetRoles();
        IQueryable<MTOrderStatu> OrderStatus();
        IQueryable<District> GetDistricts();
        IQueryable<Product> GetProducts();
    }
}
