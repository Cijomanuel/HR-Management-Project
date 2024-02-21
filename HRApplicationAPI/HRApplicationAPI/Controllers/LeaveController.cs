using HRApplicationAPI.Core.Data.Entities.Common;
using HRApplicationAPI.Core.Data.RepositoryInterfaces;
using HRApplicationAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRApplicationAPI.Controllers
{
    //LeaveController with CRUD operations

    [Route("api/[controller]")]
    [ApiController]
    public class LeaveController : ControllerBase
    {
        private readonly IRepository<Leave> rRepository;
        private readonly ILeaveRepository repository;

        //Here, Generic repository is used along with specific repo for performing join operations
        public LeaveController(IRepository<Leave> rRepository, ILeaveRepository repository)
        {
            this.rRepository = rRepository;
            this.repository = repository;
        }
        // GET: api/<LeaveController>
        [HttpGet]
        public Result<List<Leave>> GetAsync(string? Search)
        {
            var response = new Result<List<Leave>>() { Response = new List<Leave>() };
            try
            {
                if (string.IsNullOrEmpty(Search))
                {
                    var result = repository.GetAll();
                    response.Response = result;
                }
                else
                {
                    var tmp = Convert.ToInt32(Search);
                    var result =  repository.GetById(tmp);

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

        // GET api/<LeaveController>/5
        [HttpGet("{id}")]
        public Leave Get(int id)
        {
            var listOfLeaves = repository.GetById(id);
            return listOfLeaves[0];
        }

        // POST api/<LeaveController>
        [HttpPost]
        public void Post([FromBody] Leave leave)
        {
            rRepository.Add(leave);
        }

        // PUT api/<LeaveController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Leave leave)
        {
            rRepository.Update(leave);
        }

        // DELETE api/<LeaveController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            rRepository.Delete(id);
        }
    }
}
