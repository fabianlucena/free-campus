using Microsoft.EntityFrameworkCore;
using RCBACEF.IRepository;
using RCBACEF.Models;
using RCBACEF.QueryOptions;

namespace RCBACEF.Repository
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(DbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<string>> GetListNameByIdAsync(IEnumerable<Int64> ids, RoleQueryOptions? options = null)
        {
            var set = CreateDBSet(options);
            var list = await set
                .Where(r => ids.Contains(r.Id))
                .Select(r => r.Name)
                .ToListAsync();

            return list;
        }
    }
}
