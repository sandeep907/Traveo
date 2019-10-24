using Shipping.Utilities;
using Shipping.ViewModels.Roles;
using System.Collections.Generic;

namespace Shipping.Service.Interface
{
    public interface IRolesService
    {
        IEnumerable<RolesViewModel> GetAll();
        RolesViewModel GetById(int id);
        IEnumerable<RolesViewModel> GetAll(string search);
        ProcResult Add(RolesViewModel model);
        ProcResult Update(RolesViewModel model);
    }
}
