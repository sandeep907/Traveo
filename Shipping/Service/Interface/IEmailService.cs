using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shipping.Utilities;
using Shipping.ViewModels.Account;

namespace Shipping.Service.Interface
{
    public interface IEmailService
    {
        IndexEmailModel GetAll();
        EmailViewModel GetEmailById(int id);
        SentEmailViewModel GetSentEmailById(int id);
        ProcResult AddEmail(EmailViewModel model);
        ProcResult AddSentEmails(List<SentEmailViewModel> model);
    }
}
