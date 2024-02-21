namespace HRApplicationAPI.Core.Data.Entities.ViewModel
{
    public class UserClaimViewModel
    {
        public UserClaimViewModel()
        {
            Claims = new List<UserClaim>();
        }
        public string UserId { get; set; }
        public List<UserClaim> Claims { get; set; }
    }
}
