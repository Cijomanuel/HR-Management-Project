using HRApplicationAPI.Core.Data.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace HRApplicationAPI.Core.Data.Repositories
{
    public class UserValidateRepository : IUserValidateRepository
    {
        private readonly HrDbContext context;

        public UserValidateRepository(HrDbContext context)
        {
            this.context = context;
        }
        public async Task<bool> isValidateUser(string userName, string password)
        {
            try
            {
                return await context.Users.AnyAsync(u => u.UserName == userName && u.Password == password);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
