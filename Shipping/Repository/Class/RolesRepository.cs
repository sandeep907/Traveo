using Shipping.Repository.Interface;
using Shipping.Utilities;
using System;
using System.Linq;

namespace Shipping.Repository.Class
{
    public class RolesRepository: IRolesRepository
    {
        #region "Initialisation"
        private readonly ShippingEntities _context;
        public RolesRepository()
        {
            try
            { _context = new ShippingEntities(); }
            catch (Exception ex)
            { var abc = ex.Message; }
        }
        #endregion

        #region "Methods"
        public IQueryable<MTRoleMaster> GetRoles()
        {
            return _context.MTRoleMasters;
        }
        public MTRoleMaster GetRoleById(int id)
        {
            return _context.MTRoleMasters.Where(m => m.RoleId == id).FirstOrDefault();
        }
        public ProcResult AddRole(MTRoleMaster role)
        {
            ProcResult res = new ProcResult();
            try
            {
                _context.MTRoleMasters.Add(role);
                _context.SaveChanges();
                res.ErrorID = 0;
                res.strResult = "Role Added Sucessfully";
            }
            catch
            {
                res.ErrorID = 1;
                res.strResult = "Role Add Failed";
            }
            return res;
        }
        public ProcResult UpdateRole(MTRoleMaster role)
        {
            ProcResult res = new ProcResult();
            MTRoleMaster obj = new MTRoleMaster();
            try
            {
                obj = _context.MTRoleMasters.Find(role.RoleId);
                obj.RoleId = role.RoleId;
                obj.RoleName = role.RoleName;
                obj.RoleDescription = role.RoleDescription;
                obj.UID = role.UID;
                obj.UIDDate = role.UIDDate;
                _context.SaveChanges();
                res.ErrorID = 0;
                res.strResult = "Role Updated Sucessfully";
            }
            catch
            {
                res.ErrorID = 1;
                res.strResult = "Role Update Failed";
            }
            return res;
        }
        public ProcResult DeleteRole(MTRoleMaster role)
        {
            ProcResult res = new ProcResult();
            try
            {

                _context.MTRoleMasters.Attach(role);
                _context.MTRoleMasters.Remove(role);
                _context.SaveChanges();
                res.ErrorID = 0;
                res.strResult = "Role Updated Sucessfully";

            }
            catch
            {
                res.ErrorID = 1;
                res.strResult = "Role Update Failed";
            }
            return res;
        }
        #endregion

    }
}