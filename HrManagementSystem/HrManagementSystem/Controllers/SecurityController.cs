using HRApplicationAPI.Core.Data.Entities.Model;
using HRApplicationAPI.Core.Data.Entities.ViewModel;
using HrManagementSystem.Core;
using HrManagementSystem.Core.Constant;
using HrManagementSystem.Core.Data.Entities.Common;
using HrManagementSystem.Core.GuidGenerator;
using HrManagementSystem.Core.HttpClients;
using HrManagementSystem.Core.Request;
using HrManagementSystem.Core.Response.Security;
using HrManagementSystem.Core.Startup;
using HrManagementSystem.ViewModels.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;

namespace HrManagementSystem.Controllers
{
    //Client Controller to handle login, logout, registeration etc
    public class SecurityController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly IGenericHttpClient genericHttpClient;
        private readonly HttpClient httpClient;
        private readonly ITimeBasedGuidGenerator _timeBasedGuidGenerator;
        private readonly TimeBasedKeyForRequestReplay _timeBasedKeyConfiguration;
        private readonly IHttpContextAccessor httpContextAccessor;

        public SecurityController(IConfiguration configuration, IGenericHttpClient genericHttpClient, HttpClient httpClient, ITimeBasedGuidGenerator _timeBasedGuidGenerator,
            TimeBasedKeyForRequestReplay _timeBasedKeyConfiguration, IHttpContextAccessor httpContextAccessor)
        {
            this.configuration = configuration;
            this.genericHttpClient = genericHttpClient;
            this.httpClient = httpClient;
            this._timeBasedGuidGenerator = _timeBasedGuidGenerator;
            this._timeBasedKeyConfiguration = _timeBasedKeyConfiguration;
            this.httpContextAccessor = httpContextAccessor;
        }
        [HttpGet, AllowAnonymous]
        public IActionResult Login()
        {

            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Security");
            }
            var model = new LoginViewModel() { RandomSeed = GetRandomSeed(), Newkey = _timeBasedKeyConfiguration.KeyValiditySeconds * 1000 };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");

            }


            if (ModelState.IsValid)
            {
                Result<LoginResult> result = null;
                LoginRequest data = new LoginRequest();
                data.Email = model.Email;

                //Password Excryption

                data.Password = EncryptionAesCryptoServiceUtility.EncryptString(model.Password, "P@$$w0rd");

                result = await genericHttpClient.PostAsync<Result<LoginResult>>(ApiConstants.authenticateUser, data);

                if (result?.IsError == false)
                {
                    var response = result.Response.UserResponse;
                    HttpContext.Session.SetString("userId", response.UserId);
                    HttpContext.Session.SetString("empId", response.EmployeeId.ToString());

                    // CreateIdentity: function call to implement claims in the client side

                    var createdIdentity = await CreateIdentity(response);
                    if (!createdIdentity)
                    {
                        ModelState.AddModelError("", $@"Some error occured behalf of creating identity.");
                    }
                    if (!string.IsNullOrWhiteSpace(result?.WarningMessage))
                    {
                        return RedirectToAction("Login", "Security");
                    }
                    return RedirectToAction("Index", "Home");
                }
                else if (result == null || result?.IsError == true)
                {
                    ModelState.AddModelError("", $"Invalid login attempt, Please try again.");
                }
            }

            model.RandomSeed = GetRandomSeed();
            model.Newkey = _timeBasedKeyConfiguration.KeyValiditySeconds * 1000;
            ModelState.Remove(nameof(LoginViewModel.RandomSeed));

            return RedirectToAction("Index", "Home");
        }

        private async Task<bool> CreateIdentity(UserResponse response)
        {
            var result = false;
            if (response == null)
            {
                return result;
            }

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
            identity.AddClaim(new Claim(ClaimTypes.Email, response.Email ?? string.Empty));
            identity.AddClaim(new Claim(ClaimFormat.UserId, response.UserId ?? string.Empty));
            identity.AddClaim(new Claim(ClaimFormat.ClaimType1, response.Roles.Count == 1 ? response.Roles[0].Value ?? string.Empty : string.Empty));
            identity.AddClaim(new Claim(ClaimFormat.ClaimType2, response.Roles.Count == 2 ? response.Roles[1].Value ?? string.Empty : string.Empty));
            identity.AddClaim(new Claim(ClaimFormat.ClaimType3, response.Roles.Count == 3 ? response.Roles[2].Value ?? string.Empty : string.Empty));


            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

           

            result = true;
            return result;
        }

        public class UserInfoClaims : IClaimsTransformation
        {
            public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
            {
                return Task.FromResult(principal);
            }
        }
        private string GetRandomSeed()
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(_timeBasedGuidGenerator.GenerateTimeBasedGuid(DateTime.UtcNow).ToString());
            var key = Convert.ToBase64String(plainTextBytes);

            return key;
        }
        //Error Page View
        public IActionResult Error401()
        {
            return View();
        }
        //Action method to logout user and to clear cookies
        public async Task<IActionResult> Logout()
        {
            var result = await genericHttpClient.GetAsync<string>(ApiConstants.logoutApi);
            string value = HttpContext.Session.GetString("userId");

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login", "Security");

        }
        public IActionResult Test()
        {
            return View();
        }
    }
}
