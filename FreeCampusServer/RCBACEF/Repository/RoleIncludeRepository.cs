using Microsoft.EntityFrameworkCore;
using RCBACEF.IRepository;
using RCBACEF.Models;
using RCBACEF.QueryOptions;

namespace RCBACEF.Repository
{

    public class RoleIncludeRepository
        : SoftDeletableJoinRepository<RoleInclude>,
        IRoleIncludeRepository
    {
        public RoleIncludeRepository(DbContext context) : base(context)
        {
        }

        public override IQueryable<RoleInclude> CreateDBSet(BaseQueryOptions? options = null)
        {
            var quereable = base.CreateDBSet(options ?? new BaseQueryOptions());

            if (options is RoleIncludeQueryOptions sessionOptions)
            {
                if (sessionOptions.IncludeRole)
                {
                    quereable = quereable.Include(r => r.Role);
                }

                if (sessionOptions.IncludeInclude)
                {
                    quereable = quereable.Include(r => r.Include);
                }
            }

            return quereable;
        }

        public async Task<IEnumerable<Int64>> GetAllRolesIdByRolesIdAsync(IEnumerable<Int64> rolesId, RoleIncludeQueryOptions? options = null)
        {
            var set = CreateDBSet(options);
            var result = rolesId.ToList();
            var lastResult = rolesId.ToList();

            do
            {
                lastResult = await set
                    .Where(r => lastResult.Contains(r.RoleId)
                        && !result.Contains(r.RoleId)
                    )
                    .Select(r => r.IncludeId)
                    .ToListAsync();

                result.AddRange(lastResult);
            } while (lastResult.Count != 0);

            return result;
        }
    }
}
