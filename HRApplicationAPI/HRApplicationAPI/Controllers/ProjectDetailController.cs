using HRApplicationAPI.Core.Data.Entities.Common;
using HRApplicationAPI.Core.Data.RepositoryInterfaces;
using HRApplicationAPI.Model;
using Microsoft.AspNetCore.Mvc;

namespace HRApplicationAPI.Controllers
{
    //ProjectDetailController with CRUD operations

    [Route("api/[controller]")]
    [ApiController]
    public class ProjectDetailController : Controller
    {
        private readonly IRepository<ProjectDetail> rRepository;
        private readonly IProjectDetailRepository repository;

        //Here, Generic repository is used along with specific repo for performing join operations
        public ProjectDetailController(IRepository<ProjectDetail> rRepository,IProjectDetailRepository repository)
        {
            this.rRepository = rRepository;
            this.repository = repository;
        }
        // GET: api/<ProjectDetailController>
        [HttpGet]
        public Result<List<ProjectDetail>> GetAsync(string? Search)
        {
            var response = new Result<List<ProjectDetail>>() { Response = new List<ProjectDetail>() };
            try
            {
                if (string.IsNullOrEmpty(Search))
                {
                    var result = repository.GetAll();
                    response.Response = result;
                }
                else
                {
                    var result = repository.GetById(Search);

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

        // GET api/<ProjectDetailController>/5
        [HttpGet("{id}")]
        public ProjectDetail Get(int id)
        {
            return rRepository.GetById(id);
        }

        // POST api/<ProjectDetailController>
        [HttpPost]
        public void Post([FromBody] ProjectDetail projectDetail)
        {
            rRepository.Add(projectDetail);
        }

        // PUT api/<ProjectDetailController>/5
        [HttpPut]
        public void Put(int id, [FromBody] ProjectDetail projectDetail)
        {
            rRepository.Update(projectDetail);
        }

        // DELETE api/<ProjectDetailController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            rRepository.Delete(id);
        }
    }
}
