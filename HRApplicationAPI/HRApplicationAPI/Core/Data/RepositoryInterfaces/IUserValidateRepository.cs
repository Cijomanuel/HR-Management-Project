namespace HRApplicationAPI.Core.Data.RepositoryInterfaces
{
    public interface IUserValidateRepository
    {
        Task<bool> isValidateUser(string userName, string password);

    }
}
