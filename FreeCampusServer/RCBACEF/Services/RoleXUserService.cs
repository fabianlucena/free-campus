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
        public async Task<IEnumerable<long>> GetRolesIdByUserIdCompanyIdAsync(long userId, long companyId, RoleXUserQueryOptions? options = null)
        {
            return await roleXUserRepository.GetListIdByUserIdAndCompanyIdAsync(userId, companyId, options);
        }

        public async Task<IEnumerable<long>> GetAllRolesIdByUserIdAndCompanyIdAsync(long userId, long companyId, RoleXUserQueryOptions? options = null)
        {
            var rolesId = await GetRolesIdByUserIdCompanyIdAsync(userId, companyId, options);
            var allRolesId = await roleParentService.GetAllRolesIdByRolesIdAsync(rolesId);

            return allRolesId;
        }

        public async Task<IEnumerable<string>> GetAllRolesNameByRolesIdAsync(IEnumerable<long> rolesId, RoleXUserQueryOptions? options = null)
        {
            return await roleService.GetListNameByIdAsync(rolesId);
        }

        public async Task<IEnumerable<Company>> GetCompaniesListByUserIdAsync(long userId, RoleXUserQueryOptions? options = null)
        {
            return await roleXUserRepository.GetCompaniesListByUserIdAsync(userId, options);
        }
    }
}
