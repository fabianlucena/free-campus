using Microsoft.EntityFrameworkCore;
using RCBACEF.IRepository;
using RCBACEF.Models;
using RCBACEF.QueryOptions;

namespace RCBACEF.Repository
{
    public class UserRepository(DbContext context) : IUserRepository
    {
        public async Task<IEnumerable<User>> GetListAsync(UserOptions? options = null)
        {
            options ??= new UserOptions();
            var table = context.Set<User>()
                .AsNoTracking();

            table = options.Apply(table);

            return await table.ToListAsync();
        }
    }
}
