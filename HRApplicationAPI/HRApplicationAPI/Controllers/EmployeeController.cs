using HRApplicationAPI.Core.Data.Entities.Common;
using HRApplicationAPI.Core.Data.RepositoryInterfaces;
using HRApplicationAPI.Core.Request;
using HRApplicationAPI.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace HRApplicationAPI.Controllers
{
    //EmployeeController with CRUD operations

    [Route("api/[controller]")]
    [ApiController]
    [Consumes("application/json")]
    [Produces(MediaTypeNames.Application.Json)]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository repository;
        private readonly IRepository<Employee> rRepository;
        private readonly UserManager<IdentityUser> userManager;

        //Here, Generic repository is used along with specific repo for performing join operations

        public EmployeeController(IEmployeeRepository repository, IRepository<Employee> rRepository, UserManager<IdentityUser> userManager)
        {
            this.repository = repository;
            this.rRepository = rRepository;
            this.userManager = userManager;
        }
        // GET: api/<EmployeeController>
        [HttpGet]
        public async Task<Result<List<Employee>>> Get(string? Search)
        {
            var response = new Result<List<Employee>>() { Response = new List<Employee>() };
            try
            {
                if (string.IsNullOrEmpty(Search))
                {
                    var result = repository.GetAll();
                    response.Response = result;
                }
                else
                {
                    var result = await repository.GetDetailsById(Search);

                    response.Response = result;
                }
            }
            catch (Exception ex)
            {
                response.Errors?.Add(new Error
                {
                    ErrorCode = "100",
                    ErrorMessage = ex.Message,
                    ErrorType = "Error"
                });
            }
            return response;
        }

        // GET api/<EmployeeController>/5
        [HttpGet("{id}")]
        public List<Employee> Get(int id)
        {
            return repository.GetById(id);
        }
        // GET api/<EmployeeController>/5
        [HttpPost]
        [Route("GetEmployeeDetail")]
        public Employee Post([FromBody] string id)
        {
            return rRepository.GetById(id);
        }

        //Here a new Employee is created only after the creation of a user in aspnetuser. 

        // POST api/<EmployeeController>
        [HttpPost]
        public async Task<List<Employee>> PostAsync([FromBody] UserCreationRequest data)
        {
            List<Employee> employees = new List<Employee>();
            Employee employee = new Employee()
            {
                FirstName = data.FirstName,
                LastName = data.LastName,
                DesignationId = data.DesignationId,
                DepartmentId = data.DepartmentId,
                Email = data.Email,
                MobileNumber = data.MobileNumber,
                Sex = data.Sex
            };

            IdentityUser user = new IdentityUser
            {
                UserName = employee.Email,
                Email = employee.Email
            };
            var result = await userManager.CreateAsync(user, "Pass@1");

            if (result.Succeeded)
            {
                var id = user.Id;

                //CLaim allocation with respect to selected designation

                if (data.DepartmentName == "HR")
                {
                    result = await userManager.AddClaimAsync(user, new System.Security.Claims.Claim("HrRoleClaim", "HrRoleClaim"));
                }
                else
                {
                    result = await userManager.AddClaimAsync(user, new System.Security.Claims.Claim("DeveloperRoleClaim", "DeveloperRoleClaim"));
                    result = await userManager.AddClaimAsync(user, new System.Security.Claims.Claim("TeamLeadRoleClaim", "TeamLeadRoleClaim"));

                }
                if (result.Succeeded)
                {
                    employee.UserId = id;
                    employee.JoiningDate = DateTime.Now;
                    employee.Status = "Pending";
                    return repository.Add(employee);
                }

            }
            return employees;
        }

        // PUT api/<EmployeeController>/5
        [HttpPut("{id}")]
        public Employee Put(int id, [FromBody] Employee employee)
        {
            Employee emp = new Employee();
            rRepository.Update(employee);
            return emp;
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            rRepository.Delete(id);
        }
    }
}
