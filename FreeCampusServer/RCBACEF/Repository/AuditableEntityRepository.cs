using Microsoft.EntityFrameworkCore;
using RCBACEF.Models;
using RCBACEF.QueryOptions;

namespace RCBACEF.Repository
{
    public class AuditableEntityRepository<T>
        : CreatableEntityRepository<T>
        where T : AuditableEntity, new()
    {
        public AuditableEntityRepository(DbContext context) : base(context) { }

        public override IQueryable<T> CreateDBSet(BaseQueryOptions? options)
        {
            var quereable = base.CreateDBSet(options ?? new BaseQueryOptions());

            if (options is AuditableQueryOptions auditableOptions)
            {
                if (auditableOptions.IncludeUpdatedBy)
                {
                    quereable = quereable.Include(u => u.UpdatedBy);
                }
            }

            return quereable;
        }
    }
}
