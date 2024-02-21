namespace HRApplicationAPI.Core.Response
{
    public class LoginResult
    {
        public bool RequiresTwoFactor { get; set; }

        public bool Succeeded { get; set; }

        public UserResponse UserResponse { get; set; }
    }
}
