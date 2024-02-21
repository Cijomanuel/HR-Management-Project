using HRApplicationAPI.Core.Data;
using HRApplicationAPI.Core.Data.Entities.Common;
using HRApplicationAPI.Core.Data.Entities.ViewModel;
using HRApplicationAPI.Core.Data.RepositoryInterfaces;
using HRApplicationAPI.Core.HrManagementSystem.Core;
using HRApplicationAPI.Core.Response;
using HRApplicationAPI.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


//Main controller to handle logout, login, claim creation,etc...

namespace HRApplicationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly HrDbContext dbContext;
        private readonly IEmployeeRepository repository;

        public UserManager<IdentityUser> userManager { get; }
        public SignInManager<IdentityUser> SignInManager { get; }

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, HrDbContext dbContext,IEmployeeRepository repository )
        {
            this.userManager = userManager;
            this.SignInManager = signInManager;
            this.dbContext = dbContext;
            this.repository = repository;
        }

        // POST: api/<AccountController>/Login
        [HttpPost]
        [Route("Login")]

        public async Task<Result<LoginResult>> PostAsync([FromBody] LoginViewModel model)
        {
            var response = new Result<LoginResult>() { Response = new LoginResult() };
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);

                //The encripted password from the client side is decrypted via EncryptionAesCryptoServiceUtility.DecryptString function

                var _passwordValidityCheck = await userManager.CheckPasswordAsync(user, EncryptionAesCryptoServiceUtility.DecryptString(model.Password, "P@$$w0rd"));
                if (!_passwordValidityCheck)
                {
                    if (userManager.SupportsUserLockout && await userManager.GetLockoutEnabledAsync(user))
                    {
                        await userManager.AccessFailedAsync(user);
                    }

                    if (userManager.SupportsUserLockout && await userManager.IsLockedOutAsync(user))
                    {

                        if (response.Response == null) { response.Response = new LoginResult(); }
                        if (response.Response.UserResponse == null) { response.Response.UserResponse = new UserResponse(); }

                        response.Response.UserResponse.UserId = user?.Id;

                        return response;
                    }

                }
                var result = await SignInManager.PasswordSignInAsync(model.Email, EncryptionAesCryptoServiceUtility.DecryptString(model.Password, "P@$$w0rd"), false, false);
                response.Response.Succeeded = result.Succeeded;
                List<Employee> requiredData = await repository.GetDetailsById(user.Id);
                var UserClaim = await userManager.GetClaimsAsync(user);
                response.Response.UserResponse = new UserResponse
                {
                    UserId = user?.Id,
                    Address = requiredData[0].Address,
                    Age = requiredData[0].Age,
                    BloodType = requiredData[0].BloodType,
                    Department = requiredData[0].Department,
                    DepartmentId = requiredData[0].DepartmentId,
                    Designation = requiredData[0].Designation,
                    DesignationId = requiredData[0].DesignationId,
                    Dob = requiredData[0].Dob,
                    Email = requiredData[0].Email,
                    EmployeeId = requiredData[0].EmployeeId,
                    FirstName = requiredData[0].FirstName,
                    ImageFilePath = requiredData[0].ImageFilePath,
                    JoiningDate = requiredData[0].JoiningDate,
                    LastName = requiredData[0].LastName,
                    MiddleName = requiredData[0].MiddleName,
                    MobileNumber = requiredData[0].MobileNumber,
                    ResignationDate = requiredData[0].ResignationDate,
                    Roles = UserClaim,
                    Sex = requiredData[0].Sex,
                    Status = requiredData[0].Status,
                    SupervisorId = requiredData[0].SupervisorId

                };
                return response;
            }
            response.Response.Succeeded = false;
            return response;
        }

        //User Registration( Only for demostration purpose),Not used within the client side

        [HttpPost]
        [Route("Register")]
        public async Task<bool> PostAsync([FromBody] RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };
                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    result = await userManager.AddClaimsAsync(user, model.Claims.Where(c => c.isSelected).Select(e => new System.Security.Claims.Claim(e.ClaimType, e.ClaimType)));
                    return result.Succeeded;
                }
                return false;
            }
            return false;
        }

        // GET api/<AccountController>/Logout
        [HttpGet]
        [Route("Logout")]
        public async Task<bool> GetAsync()
        {
            await SignInManager.SignOutAsync();
            return true;
        }


        // GET api/<AccountController>/ChangePassword
        [HttpPost]
        [Route("ChangePassword")]
        public async Task<bool> Post(ChangepasswordViewModel viewModel)
        {
            var user = await userManager.GetUserAsync(User);

            var result = await userManager.ChangePasswordAsync(user, viewModel.OldPassword, viewModel.NewPassword);

            if (!result.Succeeded)
            {
                return false;
            }
            return true;
        }


   



    }
}
