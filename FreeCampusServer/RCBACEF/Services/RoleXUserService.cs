using RCBACEF.IRepository;
using RCBACEF.IServices;
using RCBACEF.Models;
using RCBACEF.QueryOptions;

namespace RCBACEF.Services
{
    public class RoleXUserService(
        IRoleXUserRepository roleXUserRepository,
        IRoleIncludeService roleParentService,
        IRoleService roleService
    )
        : SoftDeletableJoinService<RoleXUser>(roleXUserRepository),
        IRoleXUserService
    {
        public async Task<IEnumerable<Int64>> GetRolesIdByUserIdAsync(Int64 userId, RoleXUserQueryOptions? options = null)
        {
            return await roleXUserRepository.GetListIdByUserIdAsync(userId, options);
        }

        public async Task<IEnumerable<Int64>> GetAllRolesIdByUserIdAsync(Int64 userId, RoleXUserQueryOptions? options = null)
        {
            var rolesId = await GetRolesIdByUserIdAsync(userId, options);
            var allRolesId = await roleParentService.GetAllRolesIdByRolesIdAsync(rolesId);

            return allRolesId;
        }

        public async Task<IEnumerable<string>> GetAllRolesNameByUserIdAsync(Int64 userId, RoleXUserQueryOptions? options = null)
        {
            var allRolesId = await GetAllRolesIdByUserIdAsync(userId, options);

            return await roleService.GetListNameByIdAsync(allRolesId);
        }
    }
}
