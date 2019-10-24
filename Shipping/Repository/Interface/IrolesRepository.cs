using Shipping.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Repository.Interface
{
    public interface IRolesRepository
    {
        IQueryable<MTRoleMaster> GetRoles();
        ProcResult DeleteRole(MTRoleMaster role);
        ProcResult AddRole(MTRoleMaster role);
        ProcResult UpdateRole(MTRoleMaster role);
        MTRoleMaster GetRoleById(int id);

    }
}
