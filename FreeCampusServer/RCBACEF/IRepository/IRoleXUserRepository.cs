using RCBACEF.Models;
using RCBACEF.QueryOptions;

namespace RCBACEF.IRepository
{
    public interface IRoleXUserRepository
        : ISoftDeletableJoinRepository<RoleXUser>
    {
        Task<IEnumerable<long>> GetListIdByUserIdAndCompanyIdAsync(long userId, long? companyId, RoleXUserQueryOptions? options = null);
        Task<IEnumerable<Company>> GetCompaniesListByUserIdAsync(long userId, RoleXUserQueryOptions? options = null);
    }
}