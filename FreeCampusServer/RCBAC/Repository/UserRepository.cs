using RCBAC.IRepository;
using RCBAC.Models;

namespace RCBAC.Repository
{
    public class UserRepository(DbContext context) : IUserRepository
    {
        public async Task<IEnumerable<User>> GetListAsync()
        {
            return await context.Users.ToListAsync();
        }
    }
}
