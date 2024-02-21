using HRApplicationAPI.Core.Data.Entities.Common;
using HRApplicationAPI.Core.Data.RepositoryInterfaces;
using HRApplicationAPI.Core.Response;
using HRApplicationAPI.Model;
using Microsoft.AspNetCore.Mvc;


namespace HRApplicationAPI.Controllers
{
    //DepartmentController with CRUD operations

    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IRepository<Department> rRepository;
        private readonly IDepartmentRepository repository;

        //Here, Generic repository is used along with specific repo for performing join operations
        public DepartmentController(IRepository<Department> rRepository, IDepartmentRepository repository)
        {
            this.rRepository = rRepository;
            this.repository = repository;
        }
        // GET: api/<DepartmentController>
        [HttpGet]
        public Result<IEnumerable<Department>> Get(string? Search)
        {
            var response = new Result<IEnumerable<Department>>() { Response = Enumerable.Empty<Department>() };
            try
            {
                if (string.IsNullOrEmpty(Search))
                {
                    var result = rRepository.GetAll();
                    response.Response = result;
                }
                else
                {
                    var result = rRepository.Find(x => x.DepartmentName.Equals(Search));
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

        // GET api/<DepartmentController>/5
        [HttpGet("{id}")]
        public Department Get(int id)
        {
            return rRepository.GetById(id);
        }
        // GET api/<DepartmentController>
        [HttpGet]
        [Route("Count")]
        public List<DepartmentCountResponse> Get()
        {
            return repository.GetCount();
        }
        // POST api/<DepartmentController>
        [HttpPost]
        public void Post([FromBody] Department department)
        {
            rRepository.Add(department);    
        }

        // PUT api/<DepartmentController>/5
        [HttpPut]
        public void Put(int id, [FromBody] Department department)
        {
            rRepository.Update(department);
        }

        // DELETE api/<DepartmentController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            rRepository.Delete(id);
        }
    }
}
