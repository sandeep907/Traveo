using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Shipping.Repository;
using Shipping.Repository.Interface;
using Shipping.Service.Interface;
using Shipping.Utilities;
using Shipping.ViewModels.Account;

namespace Shipping.Service.Class
{
    public class UserService : IUserService
    {
        public readonly IUserRepository _iUserRepository;

        public UserService(IUserRepository IUserRepository)
        {
            _iUserRepository = IUserRepository;
        }

        public IEnumerable<UserViewModel> GetAll()
        {
            IQueryable<UserViewModel> roles = _iUserRepository.GetAll().UseAsDataSource(Mapper.Configuration).For<UserViewModel>();
            return roles.ToList();
        }

        public IEnumerable<UserViewModel> GetAll(string search)
        {
            IQueryable<UserViewModel> roles = _iUserRepository.GetAll(search).UseAsDataSource(Mapper.Configuration).For<UserViewModel>();
            return roles.ToList();
        }

        public UserViewModel GetById(int id)
        {
            var user = Mapper.Map<UserViewModel>(_iUserRepository.GetById(id)) == null ? new UserViewModel() : Mapper.Map<UserViewModel>(_iUserRepository.GetById(id));
            return user;
        }

        public ProcResult Save(UserViewModel user)
        {
            
            if (user.UserId == 0)
            {
                return Mapper.Map<ProcResult>(_iUserRepository.Create(Mapper.Map<MTLoginMaster>(user)));
            }
            else
            {
                return Mapper.Map<ProcResult>(_iUserRepository.Update(Mapper.Map<MTLoginMaster>(user)));
            }
        }

        public ProcResult Delete(int id, UserViewModel user)
        {
            return Mapper.Map<ProcResult>(_iUserRepository.Delete(id, Mapper.Map<MTLoginMaster>(user)));
        }
        public ProcResult ActivateAndDeActivate(int id, UserViewModel user)
        {
            return Mapper.Map<ProcResult>(_iUserRepository.ActivateAndDeActivate(id, Mapper.Map<MTLoginMaster>(user)));
        }
        public ProcResult ChangeRole(int id, UserViewModel user)
        {
            return Mapper.Map<ProcResult>(_iUserRepository.Changerole(id, Mapper.Map<MTLoginMaster>(user)));
        }
    }
}