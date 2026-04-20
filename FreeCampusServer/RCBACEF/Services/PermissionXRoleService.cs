using RCBACEF.IRepository;
using RCBACEF.IServices;
using RCBACEF.Models;

namespace RCBACEF.Services
{
    public class PermissionXRoleService(IPermissionXRoleRepository permissionXRoleRepository)
        : SoftDeletableJoinService<PermissionXRole>(permissionXRoleRepository),
        IPermissionXRoleService
    {
        public Task<IEnumerable<string>> GetAllPermissionsNameForUserIdAsync(long UserId)
        {
            throw new NotImplementedException();
        }
    }
}
