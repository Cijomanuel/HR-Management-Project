using HRApplicationAPI.Model;
using HrManagementSystem.Core.Constant;
using HrManagementSystem.Core.Data.Entities.Common;
using HrManagementSystem.Core.DTO;
using HrManagementSystem.Core.HttpClients;
using HrManagementSystem.Core.Response;
using HrManagementSystem.Models;
using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace HrManagementSystem.Controllers
{
    //The home controller to handle all common action methods for differnet controllers

    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGenericHttpClient genericHttpClient;
        private readonly IConfiguration configuration;
        private readonly HttpClient client;

        public HomeController(ILogger<HomeController> logger, IGenericHttpClient genericHttpClient, IConfiguration configuration, HttpClient client)
        {
            _logger = logger;
            this.genericHttpClient = genericHttpClient;
            this.configuration = configuration;
            this.client = client;
        }
        [HttpGet]

        //The main page after user login

        public async Task<IActionResult> Index()
        {
            //Accessing data from session

            string userId = HttpContext.Session.GetString("userId");
            string empId = HttpContext.Session.GetString("empId");

            var newUrl = ApiConstants.getEmployeeDetail + userId; 
            var result =await  genericHttpClient.GetWithId<Result<List<EmployeeDTO>>>(newUrl);
            var user = HttpContext.User.Claims.ToList(); 
           
                ViewBag.EmpId = empId;
                if (result != null)
                {
                    List<DepartmentCountResponse> counts = new List<DepartmentCountResponse>();
                    var count = await genericHttpClient.GetAsync<List<DepartmentCountResponse>>(ApiConstants.departmentCountApi);
                    ViewBag.DepartmentCount = count;
                    DepartmentDTO test = new DepartmentDTO();
                    test.DepartmentId = Convert.ToInt32(empId);
                    test.DepartmentName = "test";
                    var attendancePresent = await genericHttpClient.PostAsync<AttendanceDTO>(ApiConstants.getPresentAttendanceApi, test);
                    if (attendancePresent.AttendenceId == 0)
                    {
                        ViewBag.AttendancePresent = null;
                    }
                    else
                    {
                        ViewBag.AttendancePresent = attendancePresent;
                    }
                    ViewBag.UserData = result;
                }


                return View();

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployeeDetails(int Id)
        {
            var url = ApiConstants.employeeApi + Convert.ToString(Id);
            var employee = await genericHttpClient.GetAsync<List<EmployeeDTO>>(url);
            return View(employee.FirstOrDefault());
        }
        [HttpGet]
        public async Task<IActionResult> CreateEmployee()
        {
            var departments = await genericHttpClient.GetAsync<Common<DepartmentDTO>>(ApiConstants.departmentApi);
            var designations = await genericHttpClient.GetAsync<Common<DesignationDTO>>(ApiConstants.designationApi);
            ViewBag.departments = departments.response;
            ViewBag.designations = designations.response;
            return View();
        }
        [HttpGet]
        public  IActionResult CreateDepartment()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> EditDepartmentDetails(int Id)
        {
            var url = ApiConstants.departmentApi + Convert.ToString(Id);
            var department = await genericHttpClient.GetAsync<DepartmentDTO>(url);
            return View(department);
        }
        [HttpGet]
        public IActionResult CreateDesignation()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> EditDesignationDetails(int Id)
        {
            var url = ApiConstants.designationApi + Convert.ToString(Id);
            var designation = await genericHttpClient.GetAsync<DesignationDTO>(url);
            return View(designation);
        }
        [HttpGet]
        public IActionResult CreateAttendanceCalendar()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> EditAttendanceCalendarDetails(int Id)
        {
            var url = ApiConstants.attendanceCalendarApi + Convert.ToString(Id);
            var attendanceCalendar = await genericHttpClient.GetAsync<AttendanceCalendarDTO>(url);
            return View(attendanceCalendar);
        }
        [HttpGet]
        public IActionResult CreateProjectDetail()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> EditProjectDetailDetails(int Id)
        {
            var url = ApiConstants.projectDetailApi + Convert.ToString(Id);
            var projectDetail = await genericHttpClient.GetAsync<ProjectDetailDTO>(url);
            return View(projectDetail);
        }
        [HttpGet]
        public IActionResult CreateLeave()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> EditLeaveDetails(int Id)
        {
            var url = ApiConstants.leaveApi + Convert.ToString(Id);
            var leave = await genericHttpClient.GetAsync<LeaveDTO>(url);
            return View(leave);
        }

        [HttpGet]
        public async Task<IActionResult> EditAttendanceDetails(int Id)
        {
            var url = ApiConstants.attendanceApi + Convert.ToString(Id);
            var attendance = await genericHttpClient.GetAsync<AttendanceDTO>(url);
            return View(attendance);
        }
        [HttpPut]
        public async Task<AttendanceDTO> AttendanceCheckInCheckOut(AttendanceDTO attendance)
        {
            var url = ApiConstants.attendanceApi + Convert.ToString(attendance.AttendenceId);
            var result = await genericHttpClient.PutAsync<AttendanceDTO>(url, attendance);
            return result;
        }
        [HttpGet]
        public async Task<IActionResult> EditSalaryDetails(int Id)
        {
            var url = ApiConstants.salaryApi + Convert.ToString(Id);
            var salary = await genericHttpClient.GetAsync<SalaryDTO>(url);
            return View(salary);
        }
        [HttpGet]
        public async Task<IActionResult> GetSalaryDetails(int Id)
        {
            var url = ApiConstants.salaryApi + Convert.ToString(Id);
            var salary = await genericHttpClient.GetAsync<SalaryDTO>(url);
            return View(salary);
        }
        [HttpGet]
        public IActionResult CreateLeaveType()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> EditLeaveTypeDetails(int Id)
        {
            var url = ApiConstants.leaveTypeApi + Convert.ToString(Id);
            var leave = await genericHttpClient.GetAsync<LeaveTypeDTO>(url);
            return View(leave);
        }
        [HttpGet]
        public async Task<IActionResult> CreateLeaveRecord()
        {
            var employee = await genericHttpClient.GetAsync<Common<EmployeeDTO>>(ApiConstants.employeeApi);
            foreach (var item in employee.response)
            {
                item.FirstName = item.FirstName + " " + item.LastName;
            }
            ViewBag.EmployeeNames = new SelectList(employee.response, "EmployeeId", "FirstName");

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
            ViewBag.LeaveTypes = leaveTypes;

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> EditLeaveRecordDetails(int Id)
        {
            var url = ApiConstants.leaveRecordApi + Convert.ToString(Id);
            var leaveRecord = await genericHttpClient.GetAsync<LeaveRecordDTO>(url);
            return View(leaveRecord);
        }
    }
}