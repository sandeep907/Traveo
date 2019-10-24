using AutoMapper;
using AutoMapper.QueryableExtensions;
using Shipping.Repository;
using Shipping.Repository.Interface;
using Shipping.Service.Interface;
using Shipping.Utilities;
using Shipping.ViewModels.Roles;
using System.Collections.Generic;
using System.Linq;

namespace Shipping.Service.Class
{
    public class RolesService:IRolesService
    {
        #region "Initialisation"
        private readonly IRolesRepository _iRolesRepository;
        public RolesService(IRolesRepository IRolesRepository)
        {
            _iRolesRepository = IRolesRepository;
        }

        #endregion
        #region "methods"
        public IEnumerable<RolesViewModel>  GetAll()
        {
            return _iRolesRepository.GetRoles().UseAsDataSource(Mapper.Configuration).For<RolesViewModel>();
        }
        public IEnumerable<RolesViewModel> GetAll(string search)
        {
            IQueryable<RolesViewModel> roles = _iRolesRepository.GetRoles().Where(m=>m.RoleName.Contains(search) || m.RoleDescription.Contains(search)).UseAsDataSource(Mapper.Configuration).For<RolesViewModel>();
            return roles.ToList();
        }
        public RolesViewModel GetById(int id)
        {
            return Mapper.Map<RolesViewModel>(_iRolesRepository.GetRoleById(id));
        }
        public ProcResult Add(RolesViewModel model)
        {
           return _iRolesRepository.AddRole(Mapper.Map<MTRoleMaster>(model));
        }
        public ProcResult Update(RolesViewModel model)
        {
            return _iRolesRepository.UpdateRole(Mapper.Map<MTRoleMaster>(model));
        }
        #endregion
    }
}
