using Microsoft.EntityFrameworkCore;
using RCBACEF.IRepository;
using RCBACEF.Models;
using RCBACEF.QueryOptions;

namespace RCBACEF.Repository
{

    public class PermissionXRoleRepository : ImmutableRepository<PermissionXRole>, IPermissionXRoleRepository
    {
        public PermissionXRoleRepository(DbContext _context) : base(_context)
        {
        }

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
    }
}
