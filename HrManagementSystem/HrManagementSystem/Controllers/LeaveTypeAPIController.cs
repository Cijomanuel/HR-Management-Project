using HRApplicationAPI.Model;
using HrManagementSystem.Core.Constant;
using HrManagementSystem.Core.DTO;
using HrManagementSystem.Core.Handlers;
using HrManagementSystem.Core.HttpClients;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HrManagementSystem.Controllers
{
    [Authorize]
    //[Authorize(Policy = "SuperAdminRolePolicy")]
    //[Authorize(Policy = "HrRolePolicy")]
    public class LeaveTypeAPIController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly IGenericHttpClient genericHttpClient;
        private readonly HttpClient client;
        private readonly AuthorizationHandlerContext context;

        public LeaveTypeAPIController(IConfiguration configuration, IGenericHttpClient genericHttpClient, HttpClient client, AuthorizationHandlerContext context)
        {
            this.configuration = configuration;
            this.genericHttpClient = genericHttpClient;
            this.client = client;
            this.context = context;
        }
        // GET: LeaveTypeAPIController
        public ActionResult Index()
        {

            return View();
        }
        // GET: LeaveTypeAPIController/LeaveTypes
        public async Task<ActionResult> LeaveTypes()
        {
            if ((User.Claims.ToList())[3].Value == "SuperAdminRoleClaim")
            {
                List<LeaveTypeDTO> leaveTypes = new List<LeaveTypeDTO>();
                var leaveType = await genericHttpClient.GetAsync<Common<LeaveTypeDTO>>(ApiConstants.leaveTypeApi);
                if (leaveType.isError)
                {
                    return BadRequest(leaveType.errors[0].errorMessage);
                }
                else
                {
                    leaveTypes = leaveType.response;
                }

                return View(leaveTypes);
            }
            else
            {
                return RedirectToAction("Error401", "Security");
            }
        }
        // POST: LeaveTypeAPIController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(LeaveTypeDTO leaveType)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await genericHttpClient.PostAsync<List<LeaveTypeDTO>>(ApiConstants.leaveTypeApi, leaveType);
                    return RedirectToAction("Index", "Home");
                }
            }
            catch
            {
                return RedirectToAction("Error401", "Home");

            }
            return RedirectToAction("Error401", "Home");
        }
        // POST: LeaveTypeAPIController/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(LeaveTypeDTO leaveType)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var url = ApiConstants.leaveTypeApi + Convert.ToString(leaveType.LeaveTypeId);
                    var result = await genericHttpClient.PutAsync<LeaveTypeDTO>(url, leaveType);
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
