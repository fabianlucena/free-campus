using Microsoft.EntityFrameworkCore;
using RCBACEF.IRepository;
using RCBACEF.Models;

namespace RCBACEF.Repository
{

    public class SessionRepository : BaseRepository<Session>, ISessionRepository
    {
        public SessionRepository(DbContext _context) : base(_context)
        {
        }

        public async Task<Session?> GetFirstOrDefaultByTokenAsync(string token)
        {
            var table = context.Set<Session>();
            var session = await table
                .Where(s => s.Token == token)
                .FirstOrDefaultAsync();

            return session;
        }
    }
}
