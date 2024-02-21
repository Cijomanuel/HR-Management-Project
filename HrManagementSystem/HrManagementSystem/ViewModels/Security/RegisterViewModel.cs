using System.ComponentModel.DataAnnotations;

namespace HrManagementSystem.ViewModels.Security
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and Confirm Password doesn't match")]
        public string ConfirmPassword { get; set; }
    }
}
