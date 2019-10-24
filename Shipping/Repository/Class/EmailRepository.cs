using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shipping.Repository.Interface;
using Shipping.Utilities;

namespace Shipping.Repository.Class
{
    public class EmailRepository: IEmailRepository
    {
        #region "Initialisation"
        private readonly ShippingEntities _context;
        public EmailRepository()
        {
            try
            { _context = new ShippingEntities(); }
            catch (Exception ex)
            { var abc = ex.Message; }
        }
        #endregion
        public IQueryable<MTEmail> GetAllEmails()
        {
           return _context.MTEmails;
        }
        public IQueryable<SentEmail> GetAllSentEmails()
        {
            return _context.SentEmails;
        }
        public MTEmail GetEmailById(int id)
        {
            return _context.MTEmails.Where(m=>m.EmaidID==id).FirstOrDefault();
        }
        public SentEmail GetSentEmailById(int id)
        {
            return _context.SentEmails.Where(m => m.Id == id).FirstOrDefault();
        }
        public ProcResult AddSentMails(List<SentEmail> lst)
        {
            ProcResult result = new ProcResult();
            try
            {
                _context.SentEmails.AddRange(lst);
                _context.SaveChanges();
                result.ErrorID = 0;
                result.strResult = "Email Added Successfully";
            }
            catch(Exception ex)
            {
                result.ErrorID = 1;
                result.strResult = ex.Message;
            }
            return result;
        }
        public ProcResult AddEmail(MTEmail email)
        {
            ProcResult result = new ProcResult();
            try
            {
                _context.MTEmails.Add(email);
                _context.SaveChanges();
                result.ErrorID = 0;
                result.strResult = "Email Added Successfully";
            }
            catch (Exception ex)
            {
                result.ErrorID = 1;
                result.strResult = ex.Message;
            }
            return result;
        }
        public ProcResult UpdateEmail(MTEmail email)
        {
            ProcResult result = new ProcResult();
            try
            {
               var obj= _context.MTEmails.Find(email.EmaidID);
                obj.EmaidID = email.EmaidID;
                obj.Email = email.Email;
                obj.Password = email.Password;
                obj.UserID = email.UserID;
                _context.SaveChanges();
                result.ErrorID = 0;
                result.strResult = "Email Updated Successfully";
            }
            catch (Exception ex)
            {
                result.ErrorID = 1;
                result.strResult = ex.Message;
            }
            return result;
        }
    }
}
