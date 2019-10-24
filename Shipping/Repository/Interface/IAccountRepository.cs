using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shipping.Utilities;

namespace Shipping.Repository.Interface
{
    public interface IAccountRepository
    {
        MTLoginMaster CheckValidation(string username, string password);
        ProcResult UpdateUserLoginCount(int id);
        ProcResult UpdateUserLastLoginDate(int id);
        DateTime LastLoginDateAndTime(int userId);
        MTRoleMaster GetAll(int userTypeId);
        ProcResult updateLoginStatus(int Id,int status);
    }
}