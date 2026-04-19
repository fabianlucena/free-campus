using RCBACEF.Models;
using RCBACEF.QueryOptions;

namespace RCBACEF.IRepository
{
    public interface IRoleXUserRepository : IImmutableRepository<RoleXUser>
    {
        Task<IEnumerable<Int64>> GetListIdByUserIdAsync(Int64 userId, RoleXUserQueryOptions? options = null);
    }
}