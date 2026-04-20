using RCBACEF.Models;

namespace RCBACEF.IServices
{
    public interface IPermissionXRoleService
        : ISoftDeletableJoinService<PermissionXRole>
    {
        Task<IEnumerable<string>> GetAllPermissionsNameForUserIdAsync(Int64 UserId);
    }
}
