using HRApplicationAPI.Core.Data.Entities.Common;
using HRApplicationAPI.Core.Data.RepositoryInterfaces;
using HRApplicationAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRApplicationAPI.Controllers
{
    //AttendanceController with CRUD operations


    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IRepository<Attendance> rRepository;
        private readonly IAttendanceRepository repository;

        //Here, Generic repository is used 
        public AttendanceController(IRepository<Attendance> rRepository,IAttendanceRepository repository)
        {
            this.rRepository = rRepository;
            this.repository = repository;
        }

        // GET: api/<AttendanceController>
        [HttpGet]
        public Result<IEnumerable<Attendance>> Get(string? Search)
        {
            
            var response = new Result<IEnumerable<Attendance>>() { Response = Enumerable.Empty<Attendance>() };
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
        
        // GET api/<AttendanceController>/5
        [HttpGet("{id}")]
        public Attendance Get(int id)
        {
            return rRepository.GetById(id);
        }
        // GET api/<AttendanceController>/5
        [HttpPost]
        [Route("GetPresentAttendance")]
        //Here Department is used only for accessing id of attendance as department id 
        public Attendance Post( [FromBody] Department department)
        {
            return repository.GetPresentAttendance(department.DepartmentId);
        }
        // POST api/<AttendanceController>
        [HttpPost]
        public void Post([FromBody] Attendance attendance)
        {
            rRepository.Add(attendance);
        }

        // PUT api/<AttendanceController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Attendance attendance)
        {
            rRepository.Update(attendance);
        }

        // DELETE api/<AttendanceController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            rRepository.Delete(id);
        }
    }
}
