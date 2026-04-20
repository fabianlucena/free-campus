using RCBACEF.Models;
using RCBACEF.QueryOptions;
using RCBACEF.Repository;

namespace RCBACEF.IServices
{
    public interface IRoleXUserService
        : ISoftDeletableJoinService<RoleXUser>
    {
        Task<IEnumerable<Int64>> GetRolesIdByUserIdCompanyIdAsync(Int64 userId, Int64? companyId, RoleXUserQueryOptions? options = null);
        Task<IEnumerable<Int64>> GetAllRolesIdByUserIdAndCompanyIdAsync(Int64 userId, Int64? companyId, RoleXUserQueryOptions? options = null);
        Task<IEnumerable<string>> GetAllRolesNameByRolesIdAsync(IEnumerable<Int64> rolesId, RoleXUserQueryOptions? options = null);
    }
}
