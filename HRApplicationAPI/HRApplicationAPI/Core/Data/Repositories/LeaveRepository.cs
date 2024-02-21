using HRApplicationAPI.Core.Data.Entities.Common;
using HRApplicationAPI.Core.Data.RepositoryInterfaces;
using HRApplicationAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace HRApplicationAPI.Core.Data.Repositories
{
    public class LeaveRepository : ILeaveRepository
    {
        private readonly HrDbContext context;

        public LeaveRepository(HrDbContext context)
        {
            this.context = context;
        }
        public List<Leave> GetAll()
        {
            try
            {
                return context.Leaves.Include(o => o.Employee).Include(o => o.LeaveType).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<Leave> GetById(int id)
        {
            try
            {
                return context.Leaves.Where(o => o.LeaveId == id).Include(o => o.Employee).Include(o => o.LeaveType).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
