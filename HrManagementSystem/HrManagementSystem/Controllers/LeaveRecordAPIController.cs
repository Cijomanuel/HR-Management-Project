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
    public class LeaveRecordAPIController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly IGenericHttpClient genericHttpClient;
        private readonly HttpClient client;

        public LeaveRecordAPIController(IConfiguration configuration, IGenericHttpClient genericHttpClient, HttpClient client)
        {
            this.configuration = configuration;
            this.genericHttpClient = genericHttpClient;
            this.client = client;
        }
        // GET: LeaveRecordAPIController
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Policy = "DeveloperRolePolicy")]

        // GET: LeaveRecordAPIController/LeaveRecords
        public async Task<ActionResult> LeaveRecords()
        {
            if ((User.Claims.ToList())[3].Value == "SuperAdminRoleClaim")
            {
                List<LeaveRecordDTO> leaveRecords = new List<LeaveRecordDTO>();
                var leaveRecord = await genericHttpClient.GetAsync<Common<LeaveRecordDTO>>(ApiConstants.leaveRecordApi);
                if (leaveRecord.isError)
                {
                    return BadRequest(leaveRecord.errors[0].errorMessage);
                }
                else
                {
                    leaveRecords = leaveRecord.response;
                }

                return View(leaveRecords);
        }
            else
            {
                return RedirectToAction("Error401", "Security");
    }

}
        // POST: LeaveRecordAPIController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(LeaveRecordDTO leaveRecord)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await genericHttpClient.PostAsync<List<LeaveRecordDTO>>(ApiConstants.leaveRecordApi, leaveRecord);
                    return RedirectToAction("Index", "Home");
                }
            }
            catch
            {
                return RedirectToAction("Error401", "Home");

            }
            return RedirectToAction("Error401", "Home");
        }
        // POST: LeaveRecordAPIController/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(LeaveRecordDTO leaveRecord)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var url = ApiConstants.leaveRecordApi + Convert.ToString(leaveRecord.LeaveRecordId);
                    var result = await genericHttpClient.PutAsync<LeaveRecordDTO>(url, leaveRecord);
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
