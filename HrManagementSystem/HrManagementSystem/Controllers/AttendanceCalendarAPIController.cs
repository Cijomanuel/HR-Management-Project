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

    //Currently not allocated to any of the pages
    public class AttendanceCalendarAPIController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly IGenericHttpClient genericHttpClient;
        private readonly HttpClient client;

        public AttendanceCalendarAPIController(IConfiguration configuration, IGenericHttpClient genericHttpClient, HttpClient client)
        {
            this.configuration = configuration;
            this.genericHttpClient = genericHttpClient;
            this.client = client;
        }
        public IActionResult Index()
        {
            return View();
        }
        // GET: AttendanceCalendarAPIController/AttendanceCalendars
        public async Task<ActionResult> AttendanceCalendars()
        {
            if((User.Claims.ToList())[3].Value == "SuperAdminRoleClaim")
            {
                List<AttendanceCalendarDTO> attendanceCalendars = new List<AttendanceCalendarDTO>();
                var attendanceCalendar = await genericHttpClient.GetAsync<Common<AttendanceCalendarDTO>>(ApiConstants.attendanceCalendarApi);
                if (attendanceCalendar.isError)
                {
                    return BadRequest(attendanceCalendar.errors[0].errorMessage);
                }
                else
                {
                    attendanceCalendars = attendanceCalendar.response;
                }

                return View(attendanceCalendars);
            }
            else
            {
                return RedirectToAction("Error401", "Security");
            }
                
        }
        // POST: AttendanceCalendarAPIController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(AttendanceCalendarDTO attendanceCalendar)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await genericHttpClient.PostAsync<List<AttendanceCalendarDTO>>(ApiConstants.attendanceCalendarApi, attendanceCalendar);
                    return RedirectToAction("Index", "Home");
                }
            }
            catch
            {
                return RedirectToAction("Error401", "Home");

            }
            return RedirectToAction("Error401", "Home");
        }
        // POST: AttendanceCalendarAPIController/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(AttendanceCalendarDTO attendanceCalendar)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var url = ApiConstants.attendanceCalendarApi + Convert.ToString(attendanceCalendar.AttendenceCalendarId);
                    var result = await genericHttpClient.PutAsync<AttendanceCalendarDTO>(url, attendanceCalendar);
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
