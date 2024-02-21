using HRApplicationAPI.Core.Data.Entities.Common;
using HRApplicationAPI.Core.Response;
using HRApplicationAPI.Model;

namespace HRApplicationAPI.Core.Data.RepositoryInterfaces
{
    public interface IDepartmentRepository
    {
        List<DepartmentCountResponse> GetCount();
    }
}
