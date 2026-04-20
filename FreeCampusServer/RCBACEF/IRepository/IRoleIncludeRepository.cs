using RCBACEF.Models;
using RCBACEF.QueryOptions;

namespace RCBACEF.IRepository
{
    public interface IRoleIncludeRepository
        : ISoftDeletableJoinRepository<RoleInclude>
    {
        Task<IEnumerable<Int64>> GetAllRolesIdByRolesIdAsync(IEnumerable<Int64> rolesId, RoleIncludeQueryOptions? options = null);
    }
}