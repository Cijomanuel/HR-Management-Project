using HRApplicationAPI.Model;
using HrManagementSystem.Core.Constant;
using HrManagementSystem.Core.DTO;
using HrManagementSystem.Core.HttpClients;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HrManagementSystem.Controllers
{
    [Authorize]
    public class ProjectDetailAPIController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly IGenericHttpClient genericHttpClient;
        private readonly HttpClient client;

        public ProjectDetailAPIController(IConfiguration configuration, IGenericHttpClient genericHttpClient, HttpClient client)
        {
            this.configuration = configuration;
            this.genericHttpClient = genericHttpClient;
            this.client = client;
        }
        public IActionResult Index()
        {
            return View();
        }
        // GET: ProjectDetailAPIController/ProjectDetails
        public async Task<ActionResult> ProjectDetails()
        {
            if ((User.Claims.ToList())[3].Value == "SuperAdminRoleClaim")
            {
                List<ProjectDetailDTO> projectDetails = new List<ProjectDetailDTO>();
                var projectDetail = await genericHttpClient.GetAsync<Common<ProjectDetailDTO>>(ApiConstants.projectDetailApi);
                if (projectDetail.isError)
                {
                    return BadRequest(projectDetail.errors[0].errorMessage);
                }
                else
                {
                    projectDetails = projectDetail.response;
                }

                return View(projectDetails);
            }
            else
            {
                return RedirectToAction("Error401", "Security");
            }
        }
        // POST: ProjectDetailAPIController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(ProjectDetailDTO projectDetail)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await genericHttpClient.PostAsync<List<ProjectDetailDTO>>(ApiConstants.projectDetailApi, projectDetail);
                    return RedirectToAction("Index", "Home");
                }
            }
            catch
            {
                return RedirectToAction("Error401", "Home");

            }
            return RedirectToAction("Error401", "Home");
        }
        // POST: ProjectDetailAPIController/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ProjectDetailDTO projectDetail)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var url = ApiConstants.projectDetailApi + Convert.ToString(projectDetail.ProjectId);
                    var result = await genericHttpClient.PutAsync<ProjectDetailDTO>(url, projectDetail);
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
