using HRApplicationAPI.Core.Data.Entities.Common;
using HRApplicationAPI.Core.Data.RepositoryInterfaces;
using HRApplicationAPI.Model;
using Microsoft.AspNetCore.Mvc;

namespace HRApplicationAPI.Controllers
{
    //DesignationController with CRUD operations

    [Route("api/[controller]")]
    [ApiController]
    public class DesignationController : Controller
    {
        private readonly IRepository<Designation> rRepository;

        //Here, Generic repository is used 
        public DesignationController(IRepository<Designation> rRepository)
        {
            this.rRepository = rRepository;
        }
        // GET: api/<DesignationController>
        [HttpGet]
        public Result<IEnumerable<Designation>> Get(string? Search)
        {
            var response = new Result<IEnumerable<Designation>>() { Response = Enumerable.Empty<Designation>() };
            try
            {
                if (string.IsNullOrEmpty(Search))
                {
                    var result = rRepository.GetAll();
                    response.Response = result;
                }
                else
                {
                    var result = rRepository.Find(x => x.DesignationName.Equals(Search));
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

        // GET api/<DesignationController>/5
        [HttpGet("{id}")]
        public Designation Get(int id)
        {
            return rRepository.GetById(id);
        }

        // POST api/<DesignationController>
        [HttpPost]
        public void Post([FromBody] Designation designation)
        {
            rRepository.Add(designation);
        }

        // PUT api/<DesignationController>/5
        [HttpPut]
        public void Put(int id, [FromBody] Designation designation)
        {
            rRepository.Update(designation);
        }

        // DELETE api/<DesignationController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            rRepository.Delete(id);
        }
    }
}
