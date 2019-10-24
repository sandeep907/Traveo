using Shipping.Utilities;
using System.Linq;

namespace Shipping.Repository.Interface
{
    public interface IRemarksRepository
    {
        IQueryable<Remark> GetRemarkById(int id);
        ProcResult AddRemark(Remark remark);
        ProcResult UpdateRemark(Remark remark);
        ProcResult DeleteRemark(Remark Remark);
    }
}