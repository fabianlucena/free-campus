using RCBACEF.Models;
using RCBACEF.QueryOptions;

namespace RCBACEF.IRepository
{
    public interface IPermissionRepository : IImmutableEntityRepository<Permission>
    {
        Task<IEnumerable<string>> GetListNameByIdAsync(IEnumerable<Int64> permissionsId, PermissionQueryOptions? options = null);
    }
}