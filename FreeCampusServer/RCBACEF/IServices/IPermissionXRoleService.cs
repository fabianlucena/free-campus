using RCBACEF.Models;
using RCBACEF.QueryOptions;

namespace RCBACEF.IServices
{
    public interface IPermissionXRoleService
        : ISoftDeletableJoinService<PermissionXRole>
    {
        Task<IEnumerable<Int64>> GetAllPermissionsIdForRolesIdAsync(IEnumerable<Int64> rolesId, PermissionXRoleQueryOptions? options = null);
        Task<IEnumerable<string>> GetAllPermissionsNameForRolesIdAsync(IEnumerable<Int64> rolesId, PermissionXRoleQueryOptions? options = null);
    }
}
