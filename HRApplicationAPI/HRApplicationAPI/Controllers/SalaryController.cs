using HRApplicationAPI.Core.Data.Entities.Common;
using HRApplicationAPI.Core.Data.RepositoryInterfaces;
using HRApplicationAPI.Model;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HRApplicationAPI.Controllers
{
    //SalaryController with CRUD operations

    [Route("api/[controller]")]
    [ApiController]
    public class SalaryController : ControllerBase
    {
        private readonly IRepository<Salary> rRepository;

        //Here, Generic repository is used 
        public SalaryController(IRepository<Salary> rRepository)
        {
            this.rRepository = rRepository;
        }
        // GET: api/<SalaryController>
        [HttpGet]
        public Result<IEnumerable<Salary>> Get(string? Search)
        {
            var response = new Result<IEnumerable<Salary>>() { Response = Enumerable.Empty<Salary>() };
            try
            {
                if (string.IsNullOrEmpty(Search))
                {
                    var result = rRepository.GetAll();
                    response.Response = result;
                }
                else
                {
                    var result = rRepository.Find(x => x.EmployeeId.Equals(Search));
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

        // GET api/<SalaryController>/5
        [HttpGet("{id}")]
        public Salary Get(int id)
        {
            return rRepository.GetById(id);
        }

        // POST api/<SalaryController>
        [HttpPost]
        public void Post([FromBody] Salary salary)
        {
            rRepository.Add(salary);
        }

        // PUT api/<SalaryController>/5
        [HttpPut]
        public void Put(int id, [FromBody] Salary salary)
        {
            rRepository.Update(salary);
        }

        // DELETE api/<SalaryController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            rRepository.Delete(id);
        }
    }
}
