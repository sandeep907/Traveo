using Shipping.Repository.Interface;
using Shipping.Service.Interface;
using Shipping.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace Shipping.Service.Class
{
    public class DataService:IDataService
    {
        public readonly IListRepository _iListRepository;
        public DataService(IListRepository IListRepository)
        {
            _iListRepository = IListRepository;

        }
        public IEnumerable<IListEntry> GetRoles()
        {
            return (from t in _iListRepository.GetRoles() select new DropDownEntry() {ID=t.RoleId,Name=t.RoleName}).ToList();
        }
        public IEnumerable<IListEntry> GetOrdersStatus()
        {
            return (from t in _iListRepository.OrderStatus() select new DropDownEntry() { ID = t.Id, Name = t.StatusName }).ToList();
        }
        public IEnumerable<IListEntry> GetDistricts()
        {
            return (from t in _iListRepository.GetDistricts() select new DropDownEntry() { ID = t.ID, Name = t.DistrictName }).ToList();
        }
        public IEnumerable<IListEntry> GetProducts()
        {
            return (from t in _iListRepository.GetProducts() select new DropDownEntry() { ID = t.Id, Name = t.ProductName }).ToList();
        }
    }
}