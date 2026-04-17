using Microsoft.EntityFrameworkCore;
using RCBACEF.Models;

namespace RCBACEF.QueryOptions
{
    public class BaseQueryOptions : QueryOptions
    {
        public bool IncludeCreatedBy { get; set; } = false;

        public new IQueryable<T> Apply<T>(IQueryable<T> query) where T : Base
        {
            query = base.Apply(query);

            if (IncludeCreatedBy)
            {
                query = query.Include(u => u.CreatedBy);
            }

            return query;
        }
    }
}
