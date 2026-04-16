using Microsoft.EntityFrameworkCore;
using RCBACEF.IRepository;
using RCBACEF.Models;

namespace RCBACEF.Repository
{
    public class UserRepository(DbContext context) : IUserRepository
    {
        public async Task<IEnumerable<User>> GetListAsync()
        {
            return await context.Set<User>().ToListAsync();
        }
    }
}
