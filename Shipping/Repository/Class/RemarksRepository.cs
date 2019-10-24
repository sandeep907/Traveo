using Shipping.Repository.Interface;
using Shipping.Utilities;
using System;
using System.Linq;

namespace Shipping.Repository.Class
{
    public class RemarksRepository: IRemarksRepository
    {

        #region "Initialisation"
        private readonly ShippingEntities _context;
        public RemarksRepository()
        {
            try
            { _context = new ShippingEntities(); }
            catch (Exception ex)
            { var abc = ex.Message; }
        }
        #endregion

        #region "Methods"
        public IQueryable<Remark> GetRemarkById(int id)
        {
            return _context.Remarks.Where(m => m.OrderId == id);
        }
        public ProcResult AddRemark(Remark remark)
        {
            ProcResult res = new ProcResult();
            try
            {
                _context.Remarks.Add(remark);
                _context.SaveChanges();
                res.ErrorID = 0;
                res.strResult = "Remark Added Sucessfully";
            }
            catch
            {
                res.ErrorID = 1;
                res.strResult = "Remark Add Failed";
            }
            return res;
        }
        public ProcResult UpdateRemark(Remark remark)
        {
            ProcResult res = new ProcResult();
            Remark obj = new Remark();
            try
            {
                obj = _context.Remarks.Find(remark.OrderId);
                obj.EID = remark.EID;
                obj.EIDDate = remark.EIDDate;
                obj.OrderId= remark.OrderId;
                obj.Comment = remark.Comment;
                obj.Id = remark.Id;
                obj = remark;
                obj.UID = remark.UID;
                obj.UIDDate = remark.UIDDate;

                _context.SaveChanges();
                res.ErrorID = 0;
                res.strResult = "Remark Updated Sucessfully";
            }
            catch
            {
                res.ErrorID = 1;
                res.strResult = "Remark Update Failed";
            }
            return res;
        }
        public ProcResult DeleteRemark(Remark Remark)
        {
            ProcResult res = new ProcResult();
            try
            {

                _context.Remarks.Attach(Remark);
                _context.Remarks.Remove(Remark);
                _context.SaveChanges();
                res.ErrorID = 0;
                res.strResult = "Remark Deleted Sucessfully";

            }
            catch
            {
                res.ErrorID = 1;
                res.strResult = "Remark Delete Failed";
            }
            return res;
        }
        #endregion
    }
}
