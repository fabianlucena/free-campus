using RCBACEF.Models;
using RCBACEF.QueryOptions;

namespace RCBACEF.IRepository
{
    public interface IRoleRepository : ISoftDeletableRepository<Role>
    {
        Task<IEnumerable<string>> GetListNameByIdAsync(IEnumerable<long> ids, RoleQueryOptions? options = null);
    }
}