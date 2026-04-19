using RCBACEF.Models;

namespace RCBACEF.IServices
{
    public interface IPermissionXRoleService : IImmutableService<PermissionXRole>
    {
        Task<IEnumerable<string>> GetAllPermissionsNameForUserIdAsync(Int64 UserId);
    }
}
