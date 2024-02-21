using HRApplicationAPI.Core.Data.Entities.Common;
using HRApplicationAPI.Model;

namespace HRApplicationAPI.Core.Data.RepositoryInterfaces
{
    public interface IProjectDetailRepository
    {
        List<ProjectDetail> GetAll();

        List<ProjectDetail> GetById(string id);
    }
}
