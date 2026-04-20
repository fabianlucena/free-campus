using Microsoft.EntityFrameworkCore;
using RCBACEF.Models;
using RCBACEF.QueryOptions;

namespace RCBACEF.Repository
{
    public class JoinRepository<T>
        : BaseRepository<T>
        where T : Join, new()
    {
        public JoinRepository(DbContext context) : base(context) { }

        public override IQueryable<T> CreateDBSet(BaseQueryOptions? options = null)
        {
            var quereable = base.CreateDBSet(options);

            return quereable;
        }
    }
}
