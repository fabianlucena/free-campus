using RCBACEF.IRepository;
using RCBACEF.IServices;
using RCBACEF.Models;
using RCBACEF.QueryOptions;

namespace RCBACEF.Services
{
    public class RoleParentService(IRoleIncludeRepository roleParentRepository) : ImmutableService<RoleInclude>(roleParentRepository), IRoleIncludeService
    {
        public async Task<IEnumerable<Int64>> GetAllRolesIdByRolesIdAsync(IEnumerable<Int64> rolesId, RoleIncludeQueryOptions? options = null)
        {
            return await roleParentRepository.GetAllRolesIdByRolesIdAsync(rolesId, options);
        }
    }
}
