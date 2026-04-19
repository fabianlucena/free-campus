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

        public override IQueryable<User> CreateDBSet(BaseQueryOptions? options)
        {
            var quereable = base.CreateDBSet(options)
                .AsNoTracking();

            return quereable;
        }

        public async Task<User> GetSingleByUsernameAsync(string username, UserQueryOptions? options = null)
        {
            return await GetSingleOrDefaultByUsernameAsync(username, options)
                ?? throw new KeyNotFoundException($"User with username '{username}' not found.");
        }

        public async Task<User?> GetSingleOrDefaultByUsernameAsync(string username, UserQueryOptions? options = null)
        {
            options ??= new UserQueryOptions();
            options.Take = 2;
            var set = CreateDBSet(options);

            var list = await set
                .Where(u => u.Username == username)
                .ToListAsync();

            if (list.Count == 0)
                return null;

            if (list.Count > 1)
            {
                throw new InvalidOperationException($"Multiple users with username '{username}' found.");
            }

            return list[0];
        }
    }
}
