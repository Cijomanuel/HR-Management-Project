using System.ComponentModel.DataAnnotations;

namespace HRApplicationAPI.Core.Data.Entities.ViewModel
{
    public class ChangepasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current Password")]
        public string OldPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]

        public string NewPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Password and Confirm Password doesn't match")]
        public string ConfirmPassword { get; set; }

    }
}
