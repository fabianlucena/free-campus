using RCBACEF.IRepository;
using RCBACEF.IServices;
using RCBACEF.Models;
using RCBACEF.QueryOptions;

namespace RCBACEF.Services
{
    public class PermissionService(IPermissionRepository permissionRepository)
        : ImmutableEntityService<Permission>(permissionRepository),
        IPermissionService
    {
        public async Task<IEnumerable<string>> GetListNameByIdAsync(IEnumerable<Int64> permissionsId, PermissionQueryOptions? options = null)
        {
            return await permissionRepository.GetListNameByIdAsync(permissionsId, options);
        }
    }
}
