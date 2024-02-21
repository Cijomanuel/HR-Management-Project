using HRApplicationAPI.Core.Data.Entities.Common;
using HRApplicationAPI.Model;

namespace HRApplicationAPI.Core.Data.RepositoryInterfaces
{
    public interface ILeaveRepository
    {
        List<Leave> GetAll();
        List<Leave> GetById(int id);
    }
}
