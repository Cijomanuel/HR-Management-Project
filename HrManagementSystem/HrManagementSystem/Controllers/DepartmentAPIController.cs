using HRApplicationAPI.Model;
using HrManagementSystem.Core.Constant;
using HrManagementSystem.Core.DTO;
using HrManagementSystem.Core.HttpClients;
using HrManagementSystem.Core.Request;
using HrManagementSystem.Core.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HrManagementSystem.Controllers
{
    [Authorize]
    //[Authorize(Policy = "SuperAdminRolePolicy")]
    //[Authorize(Policy = "HrRolePolicy")]
    public class DepartmentAPIController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly IGenericHttpClient genericHttpClient;
        private readonly HttpClient client;

        public DepartmentAPIController(IConfiguration configuration, IGenericHttpClient genericHttpClient, HttpClient client)
        {
            this.configuration = configuration;
            this.genericHttpClient = genericHttpClient;
            this.client = client;
        }
        public IActionResult Index()
        {
            return View();
        }
        // GET: DepartmentController/Departments
        public async Task<ActionResult> Departments()
        {
            if ((User.Claims.ToList())[3].Value == "SuperAdminRoleClaim")
            {
                List<DepartmentCountResponse> counts = new List<DepartmentCountResponse>();
            var count = await genericHttpClient.GetAsync<List<DepartmentCountResponse>>(ApiConstants.departmentCountApi);

            return View(count);
            }
            else
            {
                return RedirectToAction("Error401", "Security");
            }
        }
        // POST: DepartmentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(DepartmentDTO department)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await genericHttpClient.PostAsync<List<DepartmentDTO>>(ApiConstants.departmentApi, department);
                    return RedirectToAction("Index", "Home");
                }
            }
            catch
            {
                return RedirectToAction("Error401", "Home");

            }
            return RedirectToAction("Error401", "Home");
        }
        // POST: DepartmentController/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(DepartmentDTO department)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var url = ApiConstants.departmentApi + Convert.ToString(department.DepartmentId);
                    var result = await genericHttpClient.PutAsync<DepartmentDTO>(url, department);
                    return RedirectToAction("Index", "Home");
                }

            }
            catch
            {
                return RedirectToAction("Error401", "Home");
            }
            return RedirectToAction("Index");
        }
    }
}
