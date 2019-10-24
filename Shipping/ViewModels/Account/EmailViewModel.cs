using Shipping.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shipping.ViewModels.Account
{
    public class IndexEmailModel
    {
        public Pager Pager { get; set; }
        public int TotalCount { get; set; }
        public List<SentEmailViewModel> lstSentEmails { get; set; }
        public List<EmailViewModel> lstEmails { get; set; }
        public SentEmailViewModel SentEmail { get; set; }
        public EmailViewModel Email { get; set; }
    }

    public class SentEmailSearchViewModel
    {
        public int TotalCount { get; set; }
        public List<SentEmailViewModel> lstSentEmails { get; set; }
    }
    public class SentEmailViewModel : BaseViewModel
    {
        public int Id { get; set; }
        public string To { get; set; }
        public int EmailId { get; set; }
        public string CC { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        //public Nullable<int> UserID { get; set; }
    }

    public class EmailSearchViewModel
    {
        public int TotalCount { get; set; }
        public List<EmailViewModel> lstEmails { get; set; }
    }

    public class EmailViewModel : BaseViewModel
    {
        public int EmaidID { get; set; }
        public string Email { get; set; }
        //public int UserID { get; set; }
        public string Password { get; set; }

    }

}
