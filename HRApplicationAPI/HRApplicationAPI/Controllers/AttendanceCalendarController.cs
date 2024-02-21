using HRApplicationAPI.Core.Data.Entities.Common;
using HRApplicationAPI.Core.Data.RepositoryInterfaces;
using HRApplicationAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRApplicationAPI.Controllers
{
    //AttendanceCalendarController with CRUD operations


    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceCalendarController : ControllerBase
    {
        private readonly IRepository<AttendanceCalendar> rRepository;

        //Here, Generic repository is used 
        public AttendanceCalendarController( IRepository<AttendanceCalendar> rRepository)
        {
            this.rRepository = rRepository;
        }


        // GET: api/<AttendanceCalendarController>
        [HttpGet]
        public Result<IEnumerable<AttendanceCalendar>> Get(string? Search)
        {
            var response = new Result<IEnumerable<AttendanceCalendar>>() { Response = Enumerable.Empty<AttendanceCalendar>() };
            try
            {
                if (string.IsNullOrEmpty(Search))
                {
                    var result = rRepository.GetAll();
                    response.Response = result;
                }
                else
                {
                    var result = rRepository.Find(x => DateTime.Compare(x.AttendenceDate, Convert.ToDateTime(Search)) == 0);
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

        // GET api/<AttendanceCalendarController>/5
        [HttpGet("{id}")]
        public AttendanceCalendar Get(int id)
        {
            return rRepository.GetById(id);
        }

        // POST api/<AttendanceCalendarController>
        [HttpPost]
        public void Post([FromBody] AttendanceCalendar AttendanceCalendar)
        {
            rRepository.Add(AttendanceCalendar);
        }

        // PUT api/<AttendanceCalendarController>/5
        [HttpPut]
        public void Put(int id, [FromBody] AttendanceCalendar AttendanceCalendar)
        {
            rRepository.Update(AttendanceCalendar);
        }

        // DELETE api/<AttendanceCalendarController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            rRepository.Delete(id);
        }
    }
}
