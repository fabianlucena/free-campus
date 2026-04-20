using RCBACEF.Models;
using RCBACEF.QueryOptions;

namespace RCBACEF.IRepository
{
    public interface IRoleXUserRepository
        : ISoftDeletableJoinRepository<RoleXUser>
    {
        Task<IEnumerable<Int64>> GetListIdByUserIdAndCompanyIdAsync(Int64 userId, Int64? companyId, RoleXUserQueryOptions? options = null);
    }
}