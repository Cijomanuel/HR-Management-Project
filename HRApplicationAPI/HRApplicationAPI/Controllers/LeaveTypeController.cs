using HRApplicationAPI.Core.Data.Entities.Common;
using HRApplicationAPI.Core.Data.RepositoryInterfaces;
using HRApplicationAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRApplicationAPI.Controllers
{
    //LeaveTypeController with CRUD operations

    [Route("api/[controller]")]
    [ApiController]
    public class LeaveTypeController : ControllerBase
    {
        private readonly IRepository<LeaveType> rRepository;

        //Here, Generic repository is used 
        public LeaveTypeController(IRepository<LeaveType> rRepository)
        {
            this.rRepository = rRepository;
        }
        // GET: api/<LeaveTypeController>
        [HttpGet]
        public Result<IEnumerable<LeaveType>> Get(string? Search)
        {
            var response = new Result<IEnumerable<LeaveType>>() { Response = Enumerable.Empty<LeaveType>() };
            try
            {
                if (string.IsNullOrEmpty(Search))
                {
                    var result = rRepository.GetAll();
                    response.Response = result;
                }
                else
                {
                    var result = rRepository.Find(x => x.LeaveName.Equals(Search));
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

            // GET api/<LeaveTypeController>/5
        [HttpGet("{id}")]
        public LeaveType Get(int id)
        {
            return rRepository.GetById(id);
        }

        // POST api/<LeaveTypeController>
        [HttpPost]
        public void Post([FromBody] LeaveType leaveType)
        {
            rRepository.Add(leaveType);
        }

        // PUT api/<LeaveTypeController>/5
        [HttpPut]
        public void Put(int id, [FromBody] LeaveType leaveType)
        {
            rRepository.Update(leaveType);
        }

        // DELETE api/<LeaveTypeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            rRepository.Delete(id);
        }
    }
}
