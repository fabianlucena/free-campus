using RCBACEF.Models;
using RCBACEF.QueryOptions;

namespace RCBACEF.IRepository
{
    public interface IPermissionXRoleRepository
        : ISoftDeletableJoinRepository<PermissionXRole>
    {
        Task<IEnumerable<Int64>> GetAllPermissionsIdByRolesIdAsync(IEnumerable<Int64> rolesId, PermissionXRoleQueryOptions? options = null);
    }
}