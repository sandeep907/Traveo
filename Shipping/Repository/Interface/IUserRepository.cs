using System.Linq;
using Shipping.Utilities;

namespace Shipping.Repository.Interface
{
    public interface IUserRepository
    {
        bool CheckUserCredential(string userName, string password);
        MTLoginMaster GetUserDetail(string userName, string password);
        IQueryable<MTLoginMaster> GetAll();
        IQueryable<MTLoginMaster> GetAll(string search);
        MTLoginMaster GetById(int id);
        ProcResult Create(MTLoginMaster user);
        ProcResult Update(MTLoginMaster user);
        ProcResult Delete(int id, MTLoginMaster user);
        ProcResult ActivateAndDeActivate(int id, MTLoginMaster user);
        ProcResult Changerole(int id, MTLoginMaster user);
    }
}
