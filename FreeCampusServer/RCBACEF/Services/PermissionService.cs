using RCBACEF.IRepository;
using RCBACEF.IServices;
using RCBACEF.Models;

namespace RCBACEF.Services
{
    public class PermissionService(IPermissionRepository permissionRepository)
        : SoftDeletableEntityService<Permission>(permissionRepository),
        IPermissionService
    {
    }
}
