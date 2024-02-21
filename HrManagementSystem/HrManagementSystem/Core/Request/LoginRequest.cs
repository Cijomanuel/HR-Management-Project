using System.ComponentModel.DataAnnotations;

namespace HrManagementSystem.Core.Request
{
	public class LoginRequest 
	{
		[Required]
		[MaxLength(300, ErrorMessage = "Maximum Length for Password can't be more than 300.")]
		public string Email { get; set; }

		[Required]
		[MaxLength(900, ErrorMessage = "Maximum Length for Password can't be more than 900.")]
		public string Password { get; set; }
	}
}
