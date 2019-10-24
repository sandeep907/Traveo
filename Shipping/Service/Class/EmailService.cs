using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Shipping.Repository;
using Shipping.Repository.Interface;
using Shipping.Service.Interface;
using Shipping.Utilities;
using Shipping.ViewModels.Account;

namespace Shipping.Service.Class
{
    public class EmailService:IEmailService
    {
        public readonly IEmailRepository _iEmailRepository;
        public EmailService(IEmailRepository IEmailRepository)
        {
            _iEmailRepository = IEmailRepository;
        }
        public IndexEmailModel GetAll()
        {
            IndexEmailModel model = new IndexEmailModel();
            model.lstEmails = _iEmailRepository.GetAllEmails().UseAsDataSource(Mapper.Configuration).For<EmailViewModel>().ToList();
            model.lstSentEmails = _iEmailRepository.GetAllSentEmails().UseAsDataSource(Mapper.Configuration).For<SentEmailViewModel>().ToList();
            model.SentEmail = new SentEmailViewModel();
            model.Email = new EmailViewModel();
            return model;
        }
        public EmailViewModel GetEmailById(int id)
        {
            var user = Mapper.Map<EmailViewModel>(_iEmailRepository.GetEmailById(id)) == null ? new EmailViewModel() : Mapper.Map<EmailViewModel>(_iEmailRepository.GetEmailById(id));
            return user;
        }
        public SentEmailViewModel GetSentEmailById(int id)
        {
            var user = Mapper.Map<SentEmailViewModel>(_iEmailRepository.GetSentEmailById(id)) == null ? new SentEmailViewModel() : Mapper.Map<SentEmailViewModel>(_iEmailRepository.GetEmailById(id));
            return user;
        }
        public ProcResult AddEmail(EmailViewModel model)
        {
            if (model.EmaidID == 0)
            {
                return Mapper.Map<ProcResult>(_iEmailRepository.AddEmail(Mapper.Map<MTEmail>(model)));
            }
            else
            {
                return Mapper.Map<ProcResult>(_iEmailRepository.UpdateEmail(Mapper.Map<MTEmail>(model)));
            }
        }
        public ProcResult AddSentEmails(List<SentEmailViewModel> model)
        {
            return Mapper.Map<ProcResult>(_iEmailRepository.AddSentMails(Mapper.Map<List<SentEmail>>(model)));
        }
    }
}



