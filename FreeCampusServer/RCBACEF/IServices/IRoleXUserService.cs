using RCBACEF.Models;
using RCBACEF.QueryOptions;

namespace RCBACEF.IServices
{
    public interface IRoleXUserService
        : ISoftDeletableJoinService<RoleXUser>
    {
        Task <IEnumerable<string>> GetAllRolesNameByUserIdAsync(Int64 UserId, RoleXUserQueryOptions? options = null);
    }
}
