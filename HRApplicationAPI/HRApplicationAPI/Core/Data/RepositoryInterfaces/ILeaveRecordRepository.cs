using HRApplicationAPI.Core.Data.Entities.Common;
using HRApplicationAPI.Model;

namespace HRApplicationAPI.Core.Data.RepositoryInterfaces
{
    public interface ILeaveRecordRepository
    {
        List<LeaveRecord> GetAll();
        List<LeaveRecord> GetById(string id);
    }
}
