using Microsoft.EntityFrameworkCore;
using RCBACEF.IRepository;
using RCBACEF.Models;
using RCBACEF.QueryOptions;

namespace RCBACEF.Repository
{

    public class RoleXUserRepository : ImmutableRepository<RoleXUser>, IRoleXUserRepository
    {
        public RoleXUserRepository(DbContext _context) : base(_context)
        {
        }

        public override IQueryable<RoleXUser> CreateDBSet(BaseQueryOptions? options)
        {
            var quereable = base.CreateDBSet(options ?? new BaseQueryOptions());

            if (options is RoleXUserQueryOptions sessionOptions)
            {
                if (sessionOptions.IncludeRole)
                {
                    quereable = quereable.Include(r => r.Role);
                }

                if (sessionOptions.IncludeUser)
                {
                    quereable = quereable.Include(u => u.User);
                }

                if (sessionOptions.IncludeCompany)
                {
                    quereable = quereable.Include(c => c.Company);
                }
            }

            return quereable;
        }
    }
}
