using RCBACEF.Models;

namespace RCBACEF.QueryOptions
{
    public class QueryOptions
    {
        public int Take { get; set; } = 20;

        public virtual IQueryable<T> Apply<T>(IQueryable<T> query)
        {
            return query.Take(Take);
        }
    }
}
