using HRApplicationAPI.Model;
using HrManagementSystem.Core.Constant;
using HrManagementSystem.Core.DTO;
using HrManagementSystem.Core.Handlers;
using HrManagementSystem.Core.HttpClients;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HrManagementSystem.Controllers
{
    [Authorize]
    //[Authorize(Policy = "SuperAdminRolePolicy")]
    //[Authorize(Policy = "HrRolePolicy")]
    public class DesignationAPIController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly IGenericHttpClient genericHttpClient;
        private readonly HttpClient client;

        public DesignationAPIController(IConfiguration configuration, IGenericHttpClient genericHttpClient, HttpClient client)
        {
            this.configuration = configuration;
            this.genericHttpClient = genericHttpClient;
            this.client = client;
        }
        // GET: DesignationAPIController
        public ActionResult Index()
        {
            return View();
        }
        // GET: DesignationController/Departments
        public async Task<ActionResult> Designations()
        {
            if ((User.Claims.ToList())[3].Value == "SuperAdminRoleClaim")
            {
                List<DesignationDTO> designations = new List<DesignationDTO>();
                var designation = await genericHttpClient.GetAsync<Common<DesignationDTO>>(ApiConstants.designationApi);
                if (designation.isError)
                {
                    return BadRequest(designation.errors[0].errorMessage);
                }
                else
                {
                    designations = designation.response;
                }

                return View(designations);
            }
            else
            {
                return RedirectToAction("Error401", "Security");
            }

        }
        // POST: DesignationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(DesignationDTO designation)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await genericHttpClient.PostAsync<List<DesignationDTO>>(ApiConstants.designationApi, designation);
                    return RedirectToAction("Index", "Home");
                }
            }
            catch
            {
                return RedirectToAction("Error401", "Home");

            }
            return RedirectToAction("Error401", "Home");
        }
        // POST: DesignationController/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(DesignationDTO designation)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var url = ApiConstants.designationApi + Convert.ToString(designation.DesignaitionId);
                    var result = await genericHttpClient.PutAsync<DesignationDTO>(url, designation);
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
