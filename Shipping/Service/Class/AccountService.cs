using System;
using AutoMapper;
using Shipping.Repository.Interface;
using Shipping.Service.Interface;
using Shipping.Utilities;
using Shipping.ViewModels.Account;

namespace Shipping.Service.Class
{
    public class AccountService: IAccountService
    {
        public readonly IAccountRepository _iAccountRepository;

        public AccountService(IAccountRepository IAccountRepository)
        {
         
            _iAccountRepository = IAccountRepository;
        }
        public UserViewModel CheckValidation(string username, string password)
        {
            return Mapper.Map<UserViewModel>(_iAccountRepository.CheckValidation(username, password));
        }

        public ProcResult UpdateUserLoginCount(int id)
        {
            return Mapper.Map<ProcResult>(_iAccountRepository.UpdateUserLoginCount(id));
        }

        public ProcResult UpdateUserLastLoginDate(int id)
        {
            return Mapper.Map<ProcResult>(_iAccountRepository.UpdateUserLastLoginDate(id));
        }

        public ProcResult UpdateLoginSatus(int id, int status)
        {
            return Mapper.Map<ProcResult>(_iAccountRepository.updateLoginStatus(id,status));
        }

        public DateTime LastLoginDateAndTime(int userId)
        {
            return _iAccountRepository.LastLoginDateAndTime(userId);
        }
        public RoleViewModel GetAll(int userTypeId)
        {
            return Mapper.Map<RoleViewModel>(_iAccountRepository.GetAll(userTypeId));
        }
    }
}