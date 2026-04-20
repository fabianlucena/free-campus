using Microsoft.EntityFrameworkCore;
using RCBACEF.IRepository;
using RCBACEF.Models;
using RCBACEF.QueryOptions;

namespace RCBACEF.Repository
{
    public class PermissionRepository
        : CreatableEntityRepository<Permission>,
        IPermissionRepository
    {
        public PermissionRepository(DbContext context) : base(context) { }

        public async Task<IEnumerable<string>> GetListNameByIdAsync(IEnumerable<Int64> permissionsId, PermissionQueryOptions? options = null)
        {
            var set = CreateDBSet(options);
            var result = await set
                .Where(r => permissionsId.Contains(r.Id))
                .Select(r => r.Name)
                .ToListAsync();

            return result;
        }
    }
}
