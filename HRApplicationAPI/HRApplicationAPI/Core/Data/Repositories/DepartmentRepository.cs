using HRApplicationAPI.Core.Data.Entities.Common;
using HRApplicationAPI.Core.Data.RepositoryInterfaces;
using HRApplicationAPI.Core.Response;
using HRApplicationAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace HRApplicationAPI.Core.Data.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly HrDbContext context;

        public DepartmentRepository(HrDbContext context)
        {
            this.context = context;
        }
        public List<DepartmentCountResponse> GetCount()
        {
            try
            {
                var dpt = context.Departments.Include(o => o.Employees).ToList();
                List<DepartmentCountResponse> count = new List<DepartmentCountResponse>();
                foreach (var d in dpt)
                {
                    DepartmentCountResponse countResponse = new DepartmentCountResponse()
                    {
                        DepartmentId = d.DepartmentId,
                        DepartmentName = d.DepartmentName,
                        EmployeeCount = d.Employees.Count()
                    };
                    count.Add(countResponse);
                }
                return count;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
