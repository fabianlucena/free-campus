using RCBACEF.Models;
using RCBACEF.QueryOptions;

namespace RCBACEF.IServices
{
    public interface IRoleXUserService
        : ISoftDeletableJoinService<RoleXUser>
    {
        Task<IEnumerable<long>> GetRolesIdByUserIdCompanyIdAsync(long userId, long companyId, RoleXUserQueryOptions? options = null);
        Task<IEnumerable<long>> GetAllRolesIdByUserIdAndCompanyIdAsync(long userId, long companyId, RoleXUserQueryOptions? options = null);
        Task<IEnumerable<string>> GetAllRolesNameByRolesIdAsync(IEnumerable<long> rolesId, RoleXUserQueryOptions? options = null);
        Task<IEnumerable<Company>> GetCompaniesListByUserIdAsync(long userId, RoleXUserQueryOptions? options = null);
    }
}
