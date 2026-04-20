using Microsoft.EntityFrameworkCore;
using RCBACEF.IRepository;
using RCBACEF.Models;
using RCBACEF.QueryOptions;

namespace RCBACEF.Repository
{

    public class PermissionXRoleRepository
        : SoftDeletableJoinRepository<PermissionXRole>,
        IPermissionXRoleRepository
    {
        public PermissionXRoleRepository(DbContext context) : base(context) { }

        public override IQueryable<PermissionXRole> CreateDBSet(BaseQueryOptions? options)
        {
            var quereable = base.CreateDBSet(options ?? new BaseQueryOptions());

            if (options is PermissionXRoleQueryOptions sessionOptions)
            {
                if (sessionOptions.IncludePermission)
                {
                    quereable = quereable.Include(p => p.Permission);
                }

                if (sessionOptions.IncludeRole)
                {
                    quereable = quereable.Include(r => r.Role);
                }
            }

            return quereable;
        }

        public async Task<IEnumerable<Int64>> GetAllPermissionsIdByRolesIdAsync(IEnumerable<Int64> rolesId, PermissionXRoleQueryOptions? options = null)
        {
            var set = CreateDBSet(options);
            var result = await set
                .Where(r => rolesId.Contains(r.RoleId))
                .Select(r => r.PermissionId)
                .ToListAsync();

            return result;
        }
    }
}
