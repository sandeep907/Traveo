using System;
using System.Linq;
using Shipping.Repository.Interface;
using Shipping.Utilities;

namespace Shipping.Repository.Class
{
    public class AccountRepository: IAccountRepository
    {
        #region "Initialisation"
        private readonly ShippingEntities _context;
        public AccountRepository()
        {
            try
            { _context = new ShippingEntities(); }
            catch (Exception ex)
            { var abc = ex.Message; }
        }

        #endregion

        #region "Operation"
        public MTLoginMaster CheckValidation(string username, string password)
        {
            try
            {
                return _context.MTLoginMasters.Where(x => x.UserEmailAddress == username && x.UserPassword == password &&x.Flag!="D").FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ProcResult UpdateUserLoginCount(int id)
        {
            ProcResult result = new ProcResult();
            try
            {
                var user = _context.MTLoginMasters.Where(x => x.UserId == id).FirstOrDefault();
                if (user != null)
                {
                    user.Count = user.Count + 1;
                    _context.SaveChanges();
                    result.ErrorID = 0;
                    result.strResult = "User Login count updated successfully !";
                }
                else
                {
                    result.ErrorID = 1;
                    result.strResult = "No User found !";
                }

            }
            catch (Exception ex)
            {
                result.ErrorID = 1;
                result.strResult = ex.Message;
            }

            return result;
        }

        public ProcResult UpdateUserLastLoginDate(int id)
        {
            ProcResult result = new ProcResult();
            try
            {
                var user = _context.MTLoginMasters.Where(x => x.UserId == id).FirstOrDefault();
                if (user != null)
                {
                    user.LastLogin = DateTime.Now;
                    _context.SaveChanges();
                    result.ErrorID = 0;
                    result.strResult = "User Last Login Updated successfully !";
                }
                else
                {
                    result.ErrorID = 1;
                    result.strResult = "No User found !";
                }

            }
            catch (Exception ex)
            {
                result.ErrorID = 1;
                result.strResult = ex.Message;
            }

            return result;
        }

        public DateTime LastLoginDateAndTime(int userId)
        {
            var login = _context.MTLoginMasters.Where(x => x.UserId == userId).FirstOrDefault();
            if (login != null)
            {
                return login.LastLogin == null ? DateTime.Now : login.LastLogin.Value;
            }
            return DateTime.Now;
        }
        public MTRoleMaster GetAll(int userTypeId)
        {
            var Roles = _context.MTRoleMasters.Where(x => x.RoleId == userTypeId).FirstOrDefault();
            if (Roles != null)
            {
                return Roles;
            }
            return new MTRoleMaster();
        }

        public ProcResult updateLoginStatus(int Id, int status)
        {
            ProcResult result = new ProcResult();
            try
            {
                var user = _context.MTLoginMasters.Where(x => x.UserId == Id).FirstOrDefault();
                if (user != null)
                {
                    user.IsLogin = status;
                    _context.SaveChanges();
                    result.ErrorID = 0;
                    result.strResult = "User Login Status Updated successfully !";
                }
                else
                {
                    result.ErrorID = 1;
                    result.strResult = "User Login Status Failed!";
                }

            }
            catch (Exception ex)
            {
                result.ErrorID = 1;
                result.strResult = ex.Message;
            }

            return result;
        }
        #endregion
    }
}