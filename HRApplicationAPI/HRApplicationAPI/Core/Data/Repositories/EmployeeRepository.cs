using HRApplicationAPI.Core.Data.Entities.Common;
using HRApplicationAPI.Core.Data.RepositoryInterfaces;
using HRApplicationAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace HRApplicationAPI.Core.Data.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly HrDbContext context;

        public EmployeeRepository(HrDbContext context)
        {
            this.context = context;
        }

        public List<Employee> Add(Employee employee)
        {
            try
            {
                List<Employee> employees = new List<Employee>();
                context.Add(employee);
                context.SaveChanges();
                employees.Add(employee);
                return employees;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Employee> GetAll()
        {
            try
            {
                return context.Employees.Include(o => o.Department).Include(o => o.Designation).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public  List<Employee> GetById(int id)
        {
            try
            {
                return  context.Employees.Where(o => o.EmployeeId==id).Include(o => o.Department).Include(o => o.Designation).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Employee>> GetDetailsById(string Id)
        {
            try
            {
                return await context.Employees.Where(o => o.UserId.Equals(Id)).Include(o => o.Department).Include(o => o.Designation).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
