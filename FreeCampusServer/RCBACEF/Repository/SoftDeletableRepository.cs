using Microsoft.EntityFrameworkCore;
using RCBACEF.Models;
using RCBACEF.QueryOptions;

namespace RCBACEF.Repository
{
    public class SoftDeletableRepository<T>
        : AuditableEntityRepository<T>
        where T : SoftDeletable, new()
    {
        public SoftDeletableRepository(DbContext context) : base(context) { }

        public override IQueryable<T> CreateDBSet(BaseQueryOptions? options)
        {
            var quereable = base.CreateDBSet(options ?? new BaseQueryOptions());

            if (options is SoftDeletableQueryOptions softDeletableOptions)
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
