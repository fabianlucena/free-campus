using RCBACEF.Models;

namespace RCBACEF.IServices
{
    public interface IRoleXUserService : IImmutableService<RoleXUser>
    {
        Task <IEnumerable<string>> GetAllRolesNameByUserIdAsync(Int64 UserId);
    }
}
