namespace HrManagementSystem.Core.Response.Security
{
	public class LoginResult
	{
		public bool RequiresTwoFactor { get; set; }

		public bool Succeeded { get; set; }

		public UserResponse UserResponse { get; set; }
	}
}
