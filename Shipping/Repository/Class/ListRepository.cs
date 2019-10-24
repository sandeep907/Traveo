using Shipping.Repository.Interface;
using System.Linq;

namespace Shipping.Repository.Class
{
    public class ListRepository : IListRepository
    {
        public readonly ShippingEntities _iShippingEntities;
        public ListRepository(ShippingEntities ShippingEntities)
        {
            _iShippingEntities = ShippingEntities;
        }
        public IQueryable<MTRoleMaster> GetRoles()
        {
           return _iShippingEntities.MTRoleMasters.AsQueryable();
        }
        public IQueryable<MTOrderStatu> OrderStatus()
        {
            return _iShippingEntities.MTOrderStatus.AsQueryable();
        }
        public IQueryable<District> GetDistricts()
        {
            return _iShippingEntities.Districts.AsQueryable();
        }
        public IQueryable<Product> GetProducts()
        {
            return _iShippingEntities.Products.AsQueryable();
        }
    }
}