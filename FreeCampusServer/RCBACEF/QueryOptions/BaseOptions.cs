using Microsoft.EntityFrameworkCore;
using RCBACEF.Models;
using System.Reflection.Emit;

namespace RCBACEF.QueryOptions
{
    public class BaseOptions : QueryOptions
    {
        public bool IncludeCreatedBy { get; set; } = false;
        public bool IncludeUpdatedBy { get; set; } = false;

        public new IQueryable<T> Apply<T>(IQueryable<T> query) where T : Base
        {
            query = base.Apply(query);

            if (IncludeCreatedBy)
            {
                query = query.Include(u => u.CreatedBy);
            }

            if (IncludeUpdatedBy)
            {
                query = query.Include(u => u.UpdatedBy);
            }

            return query;
        }
    }
}
