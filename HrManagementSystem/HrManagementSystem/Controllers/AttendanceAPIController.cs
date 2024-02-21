using HRApplicationAPI.Model;
using HrManagementSystem.Core.Constant;
using HrManagementSystem.Core.DTO;
using HrManagementSystem.Core.HttpClients;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HrManagementSystem.Controllers
{
    [Authorize]
    public class AttendanceAPIController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly IGenericHttpClient genericHttpClient;
        private readonly HttpClient client;

        public AttendanceAPIController(IConfiguration configuration, IGenericHttpClient genericHttpClient, HttpClient client)
        {
            this.configuration = configuration;
            this.genericHttpClient = genericHttpClient;
            this.client = client;
        }
        public IActionResult Index()
        {
            return View();
        }

        // GET: AttendanceAPIController/Leaves
        public async Task<ActionResult> Attendances()
        {

            string empId = HttpContext.Session.GetString("empId");
            List<AttendanceDTO> attendances = new List<AttendanceDTO>();
            var attendance = await genericHttpClient.GetAsync<Common<AttendanceDTO>>(ApiConstants.attendanceApi);
            if (attendance.isError) 
            {
                return BadRequest(attendance.errors[0].errorMessage);
            }
            else
            {
                attendances = attendance.response.Where(x => x.EmployeeId == Convert.ToInt32(empId)).ToList();
            }

            return View(attendances);
        }
        // POST: AttendanceAPIController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(AttendanceDTO attendance)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await genericHttpClient.PostAsync<List<AttendanceDTO>>(ApiConstants.attendanceApi, attendance);
                    return RedirectToAction("Index", "Home");
                }
            }
            catch
            {
                return RedirectToAction("Error401", "Home");

            }
            return RedirectToAction("Error401", "Home");
        }
        // POST: AttendanceAPIController/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(AttendanceDTO attendance)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var url = ApiConstants.attendanceApi + Convert.ToString(attendance.AttendenceId);
                    var result = await genericHttpClient.PutAsync<AttendanceDTO>(url, attendance);
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
