using RCBACEF.IRepository;
using RCBACEF.IServices;
using RCBACEF.Models;

namespace RCBACEF.Services
{
    public class RoleXUserService(IRoleXUserRepository roleXUserRepository) : ImmutableService<RoleXUser>(roleXUserRepository), IRoleXUserService
    {
        public Task<IEnumerable<string>> GetAllRolesNameByUserIdAsync(long UserId)
        {
            throw new NotImplementedException();
        }
    }
}
