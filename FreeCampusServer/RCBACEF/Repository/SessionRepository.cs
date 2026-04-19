using Microsoft.EntityFrameworkCore;
using RCBACEF.IRepository;
using RCBACEF.Models;
using RCBACEF.QueryOptions;

namespace RCBACEF.Repository
{

    public class SessionRepository : BaseRepository<Session>, ISessionRepository
    {
        public SessionRepository(DbContext _context) : base(_context)
        {
        }

        public override IQueryable<Session> CreateDBSet(BaseQueryOptions? options)
        {
            var quereable = base.CreateDBSet(options ?? new BaseQueryOptions());

            if (options is SessionQueryOptions sessionOptions)
            {
                if (sessionOptions.IncludeUser)
                {
                    quereable = quereable.Include(u => u.User);
                }

                if (sessionOptions.IncludeDevice)
                {
                    quereable = quereable.Include(d => d.Device);
                }
            }

            return quereable;
        }

        public async Task<Session?> GetFirstOrDefaultByTokenAsync(string token, SessionQueryOptions? options = null)
        {
            var set = CreateDBSet(options);
            var session = await set
                .Where(s => s.Token == token)
                .FirstOrDefaultAsync();

            return session;
        }
    }
}
