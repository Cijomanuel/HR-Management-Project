using HRApplicationAPI.Model;
using HrManagementSystem.Core.Constant;
using HrManagementSystem.Core.DTO;
using HrManagementSystem.Core.HttpClients;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HrManagementSystem.Controllers
{
    [Authorize]
    public class LeaveAPIController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly IGenericHttpClient genericHttpClient;
        private readonly HttpClient client;

        public LeaveAPIController(IConfiguration configuration, IGenericHttpClient genericHttpClient, HttpClient client)
        {
            this.configuration = configuration;
            this.genericHttpClient = genericHttpClient;
            this.client = client;
        }
        public IActionResult Index()
        {
            return View();
        }
        // GET: LeaveAPIController/Leaves
        public async Task<ActionResult> Leaves()
        {
            if ((User.Claims.ToList())[3].Value == "SuperAdminRoleClaim")
            {
                List<LeaveDTO> leaves = new List<LeaveDTO>();
            var leave = await genericHttpClient.GetAsync<Common<LeaveDTO>>(ApiConstants.leaveApi);
            if (leave.isError)
            {
                return BadRequest(leave.errors[0].errorMessage);
            }
            else
            {
                leaves = leave.response;
            }

            return View(leaves);
            }
            else
            {
                return RedirectToAction("Error401", "Security");
            }
        }
        // POST: LeaveAPIController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(LeaveDTO leave)
        {
            try
            {
                if (ModelState.IsValid)
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
                    var requiredData1 = leaveRecords.Where(x => x.EmployeeId == leave.EmployeeId);
                    var requiredData = (from record in leaveRecords select record).Where(x=>x.EmployeeId == leave.EmployeeId && x.LeaveTypeId == leave.LeaveTypeId).ToList();
                    LeaveRecordDTO leaveTemp = new LeaveRecordDTO()
                    {
                        LeaveTypeId = leave.LeaveTypeId,
                        EmployeeId = leave.EmployeeId,
                        LeaveRecordId = requiredData[0].LeaveRecordId,
                        TotalLeaves = requiredData[0].TotalLeaves,
                        RemainingDays = requiredData[0].RemainingDays - leave.TotalDays
                    };
                    var url = ApiConstants.leaveRecordApi + Convert.ToString(leaveTemp.LeaveRecordId);
                    var result1 = await genericHttpClient.PutAsync<LeaveRecordDTO>(url, leaveTemp);

                    var result = await genericHttpClient.PostAsync<List<LeaveDTO>>(ApiConstants.leaveApi, leave);
                    return RedirectToAction("Index", "Home");
                }
            }
            catch
            {
                return RedirectToAction("Error401", "Home");

            }
            return RedirectToAction("Error401", "Home");
        }
        // POST: LeaveAPIController/EditAccept
        [HttpPost]
        public async Task<ActionResult> EditAccept(int id,int st)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var url = ApiConstants.leaveApi + Convert.ToString(id);
                    var leave = await genericHttpClient.GetAsync<LeaveDTO>(url);
                    if(st == 1)
                    {
                        leave.Status = "Accept";
                    }
                    else
                    {
                        leave.Status = "Decline";
                    }
                    var url1 = ApiConstants.leaveApi + Convert.ToString(leave.LeaveId);
                    var result = await genericHttpClient.PutAsync<ProjectDetailDTO>(url1, leave);
                    return RedirectToAction("Index", "Home");
                }

            }
            catch
            {
                return RedirectToAction("Error401", "Home");
            }
            return RedirectToAction("Index");
        }
   
        [HttpGet]
        // GET: LeaveAPIController/ApplyLeave
        public async Task<ActionResult> ApplyLeave()
        {

            string empId = HttpContext.Session.GetString("empId");
            ViewBag.EmpId = Convert.ToInt32(empId);
            var leaveType = await genericHttpClient.GetAsync<Common<LeaveTypeDTO>>(ApiConstants.leaveTypeApi);
            var url = ApiConstants.leaveRecordSearchApi + empId;
            var leaveRecord = await genericHttpClient.GetAsync<Common<LeaveRecordDTO>>(url);

            ViewBag.LeaveTypes = leaveType.response;
            ViewBag.LeaveRecords = leaveRecord.response;
            return View();
        }
    }
}
