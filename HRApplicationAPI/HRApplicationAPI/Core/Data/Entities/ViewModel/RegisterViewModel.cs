using System.ComponentModel.DataAnnotations;

namespace HRApplicationAPI.Core.Data.Entities.ViewModel
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and Confirm Password doesn't match")]
        public string ConfirmPassword { get; set; }
        public RegisterViewModel()
        {
            Claims = new List<UserClaim>();
        }
        public string UserId { get; set; }
        public List<UserClaim> Claims { get; set; }


    }
}
