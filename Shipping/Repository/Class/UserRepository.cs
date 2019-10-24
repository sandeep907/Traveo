using System;
using System.Data.Entity.Core.Objects;
using System.Linq;
using Shipping.Repository.Interface;
using Shipping.Utilities;
using static Shipping.Utilities.Enums;

namespace Shipping.Repository.Class
{
    public class UserRepository:IUserRepository
    {
        #region "Initialisation"
        private readonly ShippingEntities _context;
        public UserRepository()
        {
            try
            { _context = new ShippingEntities(); }
            catch (Exception ex)
            { var abc = ex.Message; }
        }
        #endregion

        #region "Methods"
        public bool CheckUserCredential(string userName, string password)
        {
            return _context.MTLoginMasters.Where(x => x.UserEmailAddress == userName && x.UserPassword == password).Any();
        }
        public MTLoginMaster GetUserDetail(string userName, string password)
        {
            return _context.MTLoginMasters.Where(x => x.UserEmailAddress == userName && x.UserPassword == password).FirstOrDefault();
        }
        public IQueryable<MTLoginMaster> GetAll()
        {
            return _context.MTLoginMasters;
        }
        public IQueryable<MTLoginMaster> GetAll(string search)
        {
            return _context.MTLoginMasters.Where(x => x.UserEmailAddress.Contains(search) && x.UserName.Contains(search));
        }
        public MTLoginMaster GetById(int id)
        {
            return _context.MTLoginMasters.Where(x => x.UserId == id).FirstOrDefault();
        }
        public ProcResult Create(MTLoginMaster user)
        {
            ProcResult r = new ProcResult();
            ObjectParameter objE = new ObjectParameter("ipiErrorID", typeof(int));
            ObjectParameter objR = new ObjectParameter("ipvResult", typeof(string));
            _context.UserMaster((byte)Flag.Insert, user.UserId,user.ImageUrl, user.UserEmailAddress, user.UserName, user.UserPassword,user.RoleId, user.Flag, 0, objE, objR);
            r.ErrorID = Convert.ToInt32(objE.Value);
            r.strResult = Convert.ToString(objR.Value);
            return r;
        }
        public ProcResult Update(MTLoginMaster user)
        {
            ProcResult r = new ProcResult();
            ObjectParameter objE = new ObjectParameter("ipiErrorID", typeof(int));
            ObjectParameter objR = new ObjectParameter("ipvResult", typeof(string));
            _context.UserMaster((byte)Flag.Update, user.UserId, user.ImageUrl, user.UserEmailAddress, user.UserName, user.UserPassword, user.RoleId, user.Flag, 0, objE, objR);
            r.ErrorID = Convert.ToInt32(objE.Value);
            r.strResult = Convert.ToString(objR.Value);
            return r;
        }
        public ProcResult Delete(int id, MTLoginMaster user)
        {
            ProcResult r = new ProcResult();
            ObjectParameter objE = new ObjectParameter("ipiErrorID", typeof(int));
            ObjectParameter objR = new ObjectParameter("ipvResult", typeof(string));
            _context.UserMaster((byte)Flag.Delete, user.UserId, user.ImageUrl, user.UserEmailAddress, user.UserName, user.UserPassword, user.RoleId, user.Flag, 0, objE, objR);
            r.ErrorID = Convert.ToInt32(objE.Value);
            r.strResult = Convert.ToString(objR.Value);
            return r;
        }
        public ProcResult ActivateAndDeActivate(int id, MTLoginMaster user)
        {
            ProcResult r = new ProcResult();
            ObjectParameter objE = new ObjectParameter("ipiErrorID", typeof(int));
            ObjectParameter objR = new ObjectParameter("ipvResult", typeof(string));
            _context.UserMaster((byte)Flag.Activate, user.UserId, user.ImageUrl,user.UserEmailAddress, user.UserName, user.UserPassword, user.RoleId, user.Flag, 0, objE, objR);
            r.ErrorID = Convert.ToInt32(objE.Value);
            r.strResult = Convert.ToString(objR.Value);
            return r;
        }
        public ProcResult Changerole(int id, MTLoginMaster user)
        {
            ProcResult r = new ProcResult();
            ObjectParameter objE = new ObjectParameter("ipiErrorID", typeof(int));
            ObjectParameter objR = new ObjectParameter("ipvResult", typeof(string));
            _context.UserMaster((byte)Flag.Update, user.UserId, user.ImageUrl, user.UserEmailAddress, user.UserName, user.UserPassword, user.RoleId, user.Flag, 0, objE, objR);
            r.ErrorID = Convert.ToInt32(objE.Value);
            r.strResult = Convert.ToString(objR.Value);
            return r;
        }
        #endregion
    }
} 