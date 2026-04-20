using RCBACEF.IRepository;
using RCBACEF.IServices;
using RCBACEF.Models;
using RCBACEF.QueryOptions;

namespace RCBACEF.Services
{
    public class PermissionXRoleService(IPermissionXRoleRepository permissionXRoleRepository)
        : SoftDeletableJoinService<PermissionXRole>(permissionXRoleRepository),
        IPermissionXRoleService
    {
        public async Task<IEnumerable<string>> GetAllPermissionsNameForUserIdAsync(Int64 UserId, PermissionXRoleQueryOptions? options = null)
        {
            throw new NotImplementedException();
            //var allPermissionsId = await GetAllPermissionsIdByUserIdAsync(userId, options);
            //return await roleService.GetListNameByIdAsync(allRolesId);
        }
    }
}
