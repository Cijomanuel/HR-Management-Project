using HRApplicationAPI.Core.Data.Entities.Common;
using HRApplicationAPI.Core.Data.RepositoryInterfaces;
using HRApplicationAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace HRApplicationAPI.Core.Data.Repositories
{
    public class LeaveRecordRepository : ILeaveRecordRepository 
    {
        private readonly HrDbContext context;

        public LeaveRecordRepository(HrDbContext context)
        {
            this.context = context;
        }
        public List<LeaveRecord> GetAll()
        {
            try
            {
                return context.LeaveRecords.Include(o => o.LeaveType).Include(o => o.Employee).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<LeaveRecord> GetById(string id) //Here data is accessed by emp Id
        {
            try
            {
                return context.LeaveRecords.Where(o => o.EmployeeId == Convert.ToInt32(id)).Include(o => o.LeaveType).Include(o => o.Employee).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
