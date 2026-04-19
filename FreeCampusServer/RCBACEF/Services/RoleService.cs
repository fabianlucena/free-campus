using RCBACEF.IRepository;
using RCBACEF.IServices;
using RCBACEF.Models;
using RCBACEF.QueryOptions;

namespace RCBACEF.Services
{
    public class RoleService(IRoleRepository roleRepository) : BaseService<Role>(roleRepository), IRoleService
    {
        public async Task<IEnumerable<string>> GetListNameByIdAsync(IEnumerable<Int64> ids, RoleQueryOptions? options = null)
        {
            return await roleRepository.GetListNameByIdAsync(ids, options);
        }
    }
}
