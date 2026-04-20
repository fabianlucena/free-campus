using Microsoft.EntityFrameworkCore;
using RCBACEF.Models;
using RCBACEF.QueryOptions;

namespace RCBACEF.Repository
{
    public class SoftDeletableJoinRepository<T>
        : CreatableJoinRepository<T>
        where T : SoftDeletableJoin, new()
    {
        public SoftDeletableJoinRepository(DbContext context) : base(context) { }

        public override IQueryable<T> CreateDBSet(BaseQueryOptions? options)
        {
            var quereable = base.CreateDBSet(options ?? new BaseQueryOptions());

            if (options is SoftDeletableEntityQueryOptions softDeletableOptions)
            {
                if (!softDeletableOptions.IncludeDeleted)
                {
                    quereable = quereable.Where(u => u.DeletedAt == null);
                }

                if (softDeletableOptions.IncludeDeletedBy)
                {
                    quereable = quereable.Include(u => u.DeletedBy);
                }
            }

            return quereable;
        }
    }
}
