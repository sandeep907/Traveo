using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shipping.Utilities;

namespace Shipping.Repository.Interface
{
    public interface IEmailRepository
    {
        IQueryable<MTEmail> GetAllEmails();
        IQueryable<SentEmail> GetAllSentEmails();
        MTEmail GetEmailById(int id);
        SentEmail GetSentEmailById(int id);
        ProcResult AddSentMails(List<SentEmail> lst);
        ProcResult AddEmail(MTEmail email);
        ProcResult UpdateEmail(MTEmail email);
    }
}
