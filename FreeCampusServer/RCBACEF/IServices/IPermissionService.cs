using RCBACEF.Models;
using RCBACEF.QueryOptions;

namespace RCBACEF.IServices
{
    public interface IPermissionService : IImmutableEntityService<Permission>
    {
        Task<IEnumerable<string>> GetListNameByIdAsync(IEnumerable<Int64> permissionsId, PermissionQueryOptions? options = null);
    }
}
