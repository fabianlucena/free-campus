using Microsoft.EntityFrameworkCore;
using RCBACEF.Models;

namespace RCBACEF.QueryOptions
{
    public class SoftDeletableQueryOptions : AuditableQueryOptions
    {
        public bool IncludeDeleted { get; set; } = false;
        public bool IncludeDeletedBy { get; set; } = false;

        public new IQueryable<T> Apply<T>(IQueryable<T> query) where T : SoftDeletable
        {
            query = base.Apply(query);

            if (!IncludeDeleted)
            {
                query = query.Where(u => u.DeletedAt == null);
            }

            if (IncludeDeletedBy)
            {
                query = query.Include(u => u.DeletedBy);
            }

            return query;
        }
    }
}
