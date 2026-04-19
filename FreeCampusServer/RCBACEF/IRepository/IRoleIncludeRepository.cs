using RCBACEF.Models;
using RCBACEF.QueryOptions;

namespace RCBACEF.IRepository
{
    public interface IRoleIncludeRepository : IImmutableRepository<RoleInclude>
    {
        Task<IEnumerable<Int64>> GetAllRolesIdByRolesIdAsync(IEnumerable<Int64> rolesId, RoleIncludeQueryOptions? options = null);
    }
}