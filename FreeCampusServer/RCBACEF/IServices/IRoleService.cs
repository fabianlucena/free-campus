using RCBACEF.Models;
using RCBACEF.QueryOptions;

namespace RCBACEF.IServices
{
    public interface IRoleService : ISoftDeletableEntityService<Role>
    {
        Task<IEnumerable<string>> GetListNameByIdAsync(IEnumerable<Int64> ids, RoleQueryOptions? options = null);
    }
}
