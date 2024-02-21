using HRApplicationAPI.Core.Data.Entities.Common;
using HRApplicationAPI.Core.Data.RepositoryInterfaces;
using HRApplicationAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRApplicationAPI.Controllers
{
    //ProjectMemberController with CRUD operations

    [Route("api/[controller]")]
    [ApiController]
    public class ProjectMemberController : ControllerBase
    {
        private readonly IRepository<ProjectMember> rRepository;

        //Here, Generic repository is used 
        public ProjectMemberController(IRepository<ProjectMember> rRepository)
        {
            this.rRepository = rRepository;
        }
        [HttpGet]
        public Result<IEnumerable<ProjectMember>> Get(string? Search)
        {
            var response = new Result<IEnumerable<ProjectMember>>() { Response = Enumerable.Empty<ProjectMember>() };
            try
            {
                if (string.IsNullOrEmpty(Search))
                {
                    var result = rRepository.GetAll();
                    response.Response = result;
                }
                else
                {
                    var result = rRepository.Find(x => x.ProjectId.Equals(Search));
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

        // GET api/<ProjectMemberController>/5
        [HttpGet("{id}")]
        public ProjectMember Get(int id)
        {
            return rRepository.GetById(id);
        }

        // POST api/<ProjectMemberController>
        [HttpPost]
        public void Post([FromBody] ProjectMember projectMember)
        {
            rRepository.Add(projectMember);
        }

        // PUT api/<ProjectMemberController>/5
        [HttpPut]
        public void Put(int id, [FromBody] ProjectMember projectMember)
        {
            rRepository.Update(projectMember);
        }

        // DELETE api/<ProjectMemberController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            rRepository.Delete(id);
        }
    }
}
