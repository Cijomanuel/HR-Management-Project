using HRApplicationAPI.Model;
using HrManagementSystem.Core.Constant;
using HrManagementSystem.Core.DTO;
using HrManagementSystem.Core.HttpClients;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HrManagementSystem.Controllers
{
    [Authorize]
    //[Authorize(Policy = "SuperAdminRolePolicy")]
    //[Authorize(Policy = "HrRolePolicy")]
    public class SalaryAPIController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly IGenericHttpClient genericHttpClient;
        private readonly HttpClient client;

        public SalaryAPIController(IConfiguration configuration, IGenericHttpClient genericHttpClient, HttpClient client)
        {
            this.configuration = configuration;
            this.genericHttpClient = genericHttpClient;
            this.client = client;
        }

        // GET: SalaryAPIController
        public ActionResult Index()
        {
            return View();
        }
        // GET: SalaryAPIController/Salarys
        public async Task<ActionResult> Salarys()
        {
            if ((User.Claims.ToList())[3].Value == "SuperAdminRoleClaim")
            {
                List<SalaryDTO> salarys = new List<SalaryDTO>();
            var salary = await genericHttpClient.GetAsync<Common<SalaryDTO>>(ApiConstants.salaryApi);
            if (salary.isError)
            {
                return BadRequest(salary.errors[0].errorMessage);
            }
            else
            {
                salarys = salary.response;
            }

            return View(salarys);
            }
            else
            {
                return RedirectToAction("Error401", "Security");
            }
        }
        // POST: SalaryAPIController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(SalaryDTO salary)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await genericHttpClient.PostAsync<List<SalaryDTO>>(ApiConstants.salaryApi, salary);
                    return RedirectToAction("Index", "Home");
                }
            }
            catch
            {
                return RedirectToAction("Error401", "Home");

            }
            return RedirectToAction("Error401", "Home");
        }
        // POST: SalaryAPIController/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(SalaryDTO salary)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var url = ApiConstants.salaryApi + Convert.ToString(salary.SalaryId);
                    var result = await genericHttpClient.PutAsync<SalaryDTO>(url, salary);
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
