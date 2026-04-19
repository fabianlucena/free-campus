using RCBACEF.IRepository;
using RCBACEF.IServices;
using RCBACEF.Models;
using RCBACEF.QueryOptions;

namespace RCBACEF.Services
{
    public class PermissionService(IPermissionRepository permissionRepository) : BaseService<Permission>(permissionRepository), IPermissionService
    {
    }
}
