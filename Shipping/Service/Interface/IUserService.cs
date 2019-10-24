using System.Collections.Generic;
using Shipping.Utilities;
using Shipping.ViewModels.Account;

namespace Shipping.Service.Interface
{
    public interface IUserService
    {
        IEnumerable<UserViewModel> GetAll();
        IEnumerable<UserViewModel> GetAll(string search);
        UserViewModel GetById(int id);
        ProcResult Save(UserViewModel user);
        ProcResult Delete(int id, UserViewModel user);
        ProcResult ActivateAndDeActivate(int id, UserViewModel user);
        ProcResult ChangeRole(int id, UserViewModel user);
    }
}
