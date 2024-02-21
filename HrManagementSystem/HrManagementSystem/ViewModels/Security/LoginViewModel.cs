using System.ComponentModel.DataAnnotations;

namespace HrManagementSystem.ViewModels.Security
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MaxLength(900)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        public string RandomSeed { get; set; }

        public int Newkey { get; set; }

    }
}
