using HRApplicationAPI.Model;
using HrManagementSystem.Core.Constant;
using HrManagementSystem.Core.Data.Entities.Common;
using HrManagementSystem.Core.DTO;
using HrManagementSystem.Core.HttpClients;
using HrManagementSystem.Core.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HrManagementSystem.Controllers
{
    [Authorize]
    public class EmployeeAPIController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly IGenericHttpClient genericHttpClient;
        private readonly HttpClient client;

        public EmployeeAPIController(IConfiguration configuration, IGenericHttpClient genericHttpClient, HttpClient client)
        {
            this.configuration = configuration;
            this.genericHttpClient = genericHttpClient;
            this.client = client;
        }
        // GET: EmployeeController
        public async Task<ActionResult> Index()
        {
            return View();
        }
        // GET: EmployeeController/Profile

        [HttpGet]
        public async Task<ActionResult> Profile()
        {
            var user = HttpContext.User.Claims.ToList();
            string value = HttpContext.Session.GetString("userId");
            var newUrl = ApiConstants.getEmployeeDetail + value;
            var result = await genericHttpClient.GetWithId<Result<List<EmployeeDTO>>>(newUrl);


            return View(result.Response.FirstOrDefault());
        }
        // GET: EmployeeController/Edit_Profile

        [HttpGet]
        public async Task<ActionResult> Edit_Profile()
        {
            string value = HttpContext.Session.GetString("userId");
            var newUrl = ApiConstants.getEmployeeDetail + value;
            var result = await genericHttpClient.GetWithId<Result<List<EmployeeDTO>>>(newUrl);


            return View(result.Response.FirstOrDefault());
        }
        // GET: EmployeeController/Employees

        public async Task<ActionResult> Employees ()
        {
            List<EmployeeDTO> employees = new List<EmployeeDTO>();
            var employee = await genericHttpClient.GetAsync<Common<EmployeeDTO>>(ApiConstants.employeeApi);
            string values = HttpContext.Session.GetString("myvalue");
            if (employee.isError)
            {
                RedirectToAction("Error401", "Home");
            }
            else
            {
                employees = employee.response;
            }

            return View(employees);
        }
        // GET: EmployeeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EmployeeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeeController/CreateAsync
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(UserCreationRequest user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await genericHttpClient.PostAsync<List<EmployeeDTO>>(ApiConstants.employeeApi, user);
                    return RedirectToAction("Index", "Home");
                }
            }
            catch
            {
                return RedirectToAction("Error401", "Home");

            }
                    return RedirectToAction("Error401", "Home");
        }

        // GET: EmployeeController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var departments = await genericHttpClient.GetAsync<Common<DepartmentDTO>>(ApiConstants.departmentApi);
            var designations = await genericHttpClient.GetAsync<Common<DesignationDTO>>(ApiConstants.designationApi);
            ViewBag.departments = departments.response;
            ViewBag.designations = designations.response;
            var url = ApiConstants.employeeApi + Convert.ToString(id);
            var employee = await genericHttpClient.GetAsync<List<EmployeeDTO>>(url);
            return View(employee.FirstOrDefault());
        }

        // POST: EmployeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(EmployeeDTO employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var url = ApiConstants.employeeApi + Convert.ToString(employee.EmployeeId);
                    employee.Status = "Active";
                    var result = await genericHttpClient.PutAsync<EmployeeDTO>(url, employee);
                    return RedirectToAction("Index", "Home");
                }

            }
            catch
            {
                return RedirectToAction("Error401", "Home");
            }
            return RedirectToAction("Index");
        }

        // GET: EmployeeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EmployeeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
