using Microsoft.EntityFrameworkCore;
using RCBACEF.Models;
using RCBACEF.QueryOptions;

namespace RCBACEF.Repository
{
    public class AuditableRepository<T> : CreatableEntityRepository<T> where T : Auditable, new()
    {
        public AuditableRepository(DbContext context) : base(context) { }

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
