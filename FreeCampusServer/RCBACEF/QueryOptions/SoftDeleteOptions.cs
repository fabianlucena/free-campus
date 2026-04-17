using Microsoft.EntityFrameworkCore;
using RCBACEF.Models;

namespace RCBACEF.QueryOptions
{
    public class SoftDeleteOptions : BaseOptions
    {
        public bool IncludeDeleted { get; set; } = false;
        public bool IncludeDeletedBy { get; set; } = false;

        public new IQueryable<T> Apply<T>(IQueryable<T> query) where T : SoftDelete
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
