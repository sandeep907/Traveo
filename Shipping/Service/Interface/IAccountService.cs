using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shipping.Utilities;
using Shipping.ViewModels.Account;

namespace Shipping.Service.Interface
{
    public interface IAccountService
    {
        UserViewModel CheckValidation(string username, string password);
        ProcResult UpdateUserLoginCount(int id);
        ProcResult UpdateUserLastLoginDate(int id);
        DateTime LastLoginDateAndTime(int userId);
        RoleViewModel GetAll(int userTypeId);
        ProcResult UpdateLoginSatus(int id, int status);
    }
}
