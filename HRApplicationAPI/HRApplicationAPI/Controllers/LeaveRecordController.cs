using HRApplicationAPI.Core.Data.Entities.Common;
using HRApplicationAPI.Core.Data.RepositoryInterfaces;
using HRApplicationAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRApplicationAPI.Controllers
{
    //LeaveRecordController with CRUD operations

    [Route("api/[controller]")]
    [ApiController]
    public class LeaveRecordController : ControllerBase
    {
        private readonly IRepository<LeaveRecord> rRepository;
        private readonly ILeaveRecordRepository repository;

        //Here, Generic repository is used along with specific repo for performing join operations
        public LeaveRecordController(IRepository<LeaveRecord> rRepository, ILeaveRecordRepository repository)
        {
            this.rRepository = rRepository;
            this.repository = repository;
        }
        // GET: api/<LeaveRecordController>
        [HttpGet]
        public Result<List<LeaveRecord>> GetAsync(string? Search)
        {
            var response = new Result<List<LeaveRecord>>() { Response = new List<LeaveRecord>() };
            try
            {
                if (string.IsNullOrEmpty(Search))
                {
                    var result = repository.GetAll();
                    response.Response = result;
                }
                else
                {
                    var result = repository.GetById(Search); //Here data is accessed by emp Id

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

        // GET api/<LeaveRecordController>/5
        [HttpGet("{id}")]
        public LeaveRecord Get(int id)
        {
            return rRepository.GetById(id);
        }

        // POST api/<LeaveRecordController>
        [HttpPost]
        public void Post([FromBody] LeaveRecord leaveRecord)
        {
            rRepository.Add(leaveRecord);
        }

        // PUT api/<LeaveRecordController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] LeaveRecord leaveRecord)
        {
            rRepository.Update(leaveRecord);
        }

        // DELETE api/<LeaveRecordController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            rRepository.Delete(id);
        }
    }
}
