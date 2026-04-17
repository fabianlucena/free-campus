using Microsoft.EntityFrameworkCore;
using RCBACEF.IRepository;
using RCBACEF.Models;
using RCBACEF.QueryOptions;

namespace RCBACEF.Repository
{
    public class UserRepository : SoftDeletableRepository<User>, IUserRepository
    {
        public UserRepository(DbContext _context) : base(_context)
        {
        }

        public async Task<IEnumerable<User>> GetListAsync(UserQueryOptions? options = null)
        {
            options ??= new UserQueryOptions();
            var table = context.Set<User>()
                .AsNoTracking();

            table = options.Apply(table);

            return await table.ToListAsync();
        }

        public async Task<User> GetSingleByUsernameAsync(string username, UserQueryOptions? options = null)
        {
            options ??= new UserQueryOptions();
            var table = context.Set<User>()
                .AsNoTracking();

            var list = await options.Apply(table)
                .Take(2)
                .Where(u => u.Username == username)
                .ToListAsync();

            if (list.Count == 0)
            {
                throw new KeyNotFoundException($"User with username '{username}' not found.");
            }

            if (list.Count > 1)
            {
                throw new InvalidOperationException($"Multiple users with username '{username}' found.");
            }

            return list[0];
        }
    }
}
