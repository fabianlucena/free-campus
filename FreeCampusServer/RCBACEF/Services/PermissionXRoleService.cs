using RCBACEF.IRepository;
using RCBACEF.IServices;
using RCBACEF.Models;
using RCBACEF.QueryOptions;

namespace RCBACEF.Services
{
    public class PermissionXRoleService(
        IPermissionXRoleRepository permissionXRoleRepository,
        IPermissionService permissionService
    )
        : SoftDeletableJoinService<PermissionXRole>(permissionXRoleRepository),
        IPermissionXRoleService
    {
        public async Task<IEnumerable<Int64>> GetAllPermissionsIdForRolesIdAsync(IEnumerable<Int64> rolesId, PermissionXRoleQueryOptions? options = null)
        {
            return await permissionXRoleRepository.GetAllPermissionsIdByRolesIdAsync(rolesId, options);
        }

        public async Task<IEnumerable<string>> GetAllPermissionsNameForRolesIdAsync(IEnumerable<Int64> rolesId, PermissionXRoleQueryOptions? options = null)
        {
            var allPermissionsId = await GetAllPermissionsIdForRolesIdAsync(rolesId, options);
            return await permissionService.GetListNameByIdAsync(allPermissionsId);
        }
    }
}
