using RCBACEF.Models;
using RCBACEF.Services;

namespace RCBACEF.IServices
{
    public interface IPermissionService : ISoftDeletableEntityService<Permission>
    {
        Task<IEnumerable<string>> GetListNameByIdAsync(IEnumerable<Int64> permissionsId);
    }
}
