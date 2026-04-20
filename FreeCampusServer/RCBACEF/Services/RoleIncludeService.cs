using RCBACEF.IRepository;
using RCBACEF.IServices;
using RCBACEF.Models;
using RCBACEF.QueryOptions;

namespace RCBACEF.Services
{
    public class RoleIncludeService(IRoleIncludeRepository roleIncludeRepository)
        : SoftDeletableJoinService<RoleInclude>(roleIncludeRepository),
        IRoleIncludeService
    {
        public async Task<IEnumerable<Int64>> GetAllRolesIdByRolesIdAsync(IEnumerable<Int64> rolesId, RoleIncludeQueryOptions? options = null)
        {
            return await roleIncludeRepository.GetAllRolesIdByRolesIdAsync(rolesId, options);
        }
    }
}
