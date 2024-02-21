using HRApplicationAPI.Core.Data.Entities.Common;
using HRApplicationAPI.Model;

namespace HRApplicationAPI.Core.Data.RepositoryInterfaces
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetDetailsById(string Id);
        List<Employee> GetAll();
        List<Employee> GetById(int id);
        List<Employee> Add(Employee employee);

    }
}
