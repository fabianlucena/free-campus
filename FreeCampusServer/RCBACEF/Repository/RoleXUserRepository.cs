using Microsoft.EntityFrameworkCore;
using RCBACEF.IRepository;
using RCBACEF.Models;
using RCBACEF.QueryOptions;

namespace RCBACEF.Repository
{

    public class RoleXUserRepository
        : SoftDeletableJoinRepository<RoleXUser>,
        IRoleXUserRepository
    {
        public RoleXUserRepository(DbContext context) : base(context) { }

        public override IQueryable<RoleXUser> CreateDBSet(BaseQueryOptions? options = null)
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

        public async Task<IEnumerable<Int64>> GetListIdByUserIdAndCompanyIdAsync(Int64 userId, Int64? companyId, RoleXUserQueryOptions? options = null)
        {
            var set = CreateDBSet(options);

            var list = await set
                .Where(e => e.UserId == userId && (companyId == null || e.CompanyId == companyId))
                .Select(e => e.RoleId)
                .ToListAsync();

            return list;
        }
    }
}
