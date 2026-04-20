using RCBACEF.Models;
using RCBACEF.QueryOptions;

namespace RCBACEF.IServices
{
    public interface IRoleXUserService
        : ISoftDeletableJoinService<RoleXUser>
    {
        Task <IEnumerable<string>> GetAllRolesNameByUserIdAndCompanyIdAsync(Int64 UserId, Int64? CompanyId, RoleXUserQueryOptions? options = null);
    }
}
