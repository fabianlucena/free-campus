using Microsoft.EntityFrameworkCore;
using RCBACEF.Models;
using RCBACEF.QueryOptions;

namespace RCBACEF.Repository
{
    public class AuditableRepository<T> : BaseRepository<T> where T : Auditable
    {
        public AuditableRepository(DbContext context) : base(context)
        {
        }

        public override async Task<T> ValidateForCreation(T entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;

            return entity;
        }

        public override IQueryable<T> CreateDBSet(BaseQueryOptions options)
        {
            var quereable = base.CreateDBSet(options);

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
