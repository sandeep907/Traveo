using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Shipping.Service.Interface;
using Shipping.Utilities;
using Shipping.ViewModels.Account;

namespace Shipping.Controllers
{
    [Authorize(Roles = "1")]
    public class EmailController : Controller
    {
        public readonly IEmailService _iEmailService;
        public EmailController(IEmailService IEmailService)
        {
            _iEmailService = IEmailService;
        }
        // GET: Email
        public ActionResult Index(int ? page)
        {
            IndexEmailModel model = new IndexEmailModel();
            model = _iEmailService.GetAll();
            model.TotalCount = _iEmailService.GetAll().lstSentEmails.Count();
            var pager = new Pager(model.TotalCount, page,5);
            model.lstSentEmails = _iEmailService.GetAll().lstSentEmails
                   .Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize).ToList();
            model.Pager = pager;
            return View(model);
        }
        [HttpGet]
        public ActionResult Search(int? page, int pageSize)
        {
            IndexEmailModel model = new IndexEmailModel();
            model.TotalCount = _iEmailService.GetAll().lstSentEmails.Count();
            var pager = new Pager(model.TotalCount, page, pageSize);
            model.lstSentEmails = _iEmailService.GetAll().lstSentEmails
                 .Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize).ToList();
            model.Pager = pager;
            return PartialView("_EmailList", model);
        }

        public ActionResult Send()
        {
            SentEmailViewModel model = new SentEmailViewModel();
            return View(model);

        }
        [HttpPost]
        public ActionResult Send(SentEmailViewModel model)
        {
            List<SentEmailViewModel> lst = new List<SentEmailViewModel>();
            string[] multipleTo = model.To.Split(';');
            foreach (var to in multipleTo)
            {
                lst.Add(new SentEmailViewModel() { To = to });
            }
            lst.ForEach(m => m.EmailId = model.EmailId);
            lst.ForEach(m => m.Body = model.Body);
            lst.ForEach(m => m.Subject = model.Subject);
            lst.ForEach(m => m.CC = model.CC);
            //lst.lstSentEmails.ForEach(m => m.UserID = model.UserID);
            lst.ForEach(m => m.Date = DateTime.Now);
            if (Email.send(model))
            {

                _iEmailService.AddSentEmails(lst);
            }
            return RedirectToAction("Index", "Email");
        }

        public ActionResult AddEmail()
        {
            EmailViewModel model = new EmailViewModel();
            return PartialView("_AddEmail", model);
        }
        [HttpPost]
        public ActionResult AddEmail(EmailViewModel model)
        {
            var result = _iEmailService.AddEmail(model);
            return RedirectToAction("Index", "Email");
        }
    }
}