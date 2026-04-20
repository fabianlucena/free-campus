using RCBACEF.Models;
using RCBACEF.QueryOptions;

namespace RCBACEF.IServices
{
    public interface IPermissionXRoleService
        : ISoftDeletableJoinService<PermissionXRole>
    {
        Task<IEnumerable<string>> GetAllPermissionsNameForUserIdAsync(Int64 UserId, PermissionXRoleQueryOptions? options = null);
    }
}
