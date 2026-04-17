using Microsoft.EntityFrameworkCore;
using RCBACEF.Models;

namespace RCBACEF.QueryOptions
{
    public class AuditableQueryOptions : BaseQueryOptions
    {
        public bool IncludeUpdatedBy { get; set; } = false;

        public new IQueryable<T> Apply<T>(IQueryable<T> query) where T : Auditable
        {
            query = base.Apply(query);

            if (IncludeUpdatedBy)
            {
                query = query.Include(u => u.UpdatedBy);
            }

            return query;
        }
    }
}
