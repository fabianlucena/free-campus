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

        public override async Task<Session> ValidateForCreation(Session session)
        {
            session = await base.ValidateForCreation(session);

            if (string.IsNullOrEmpty(session.Token))
            {
                throw new ArgumentException("Session token cannot be null or empty.");
            }

            if (await GetFirstOrDefaultByTokenAsync(session.Token) != null)
            {
                throw new InvalidOperationException("A session with the same token already exists.");
            }

            return session;
        }

        public async Task<Session> CreateAsync(Session session)
        {
            session = await ValidateForCreation(session);

            var table = context.Set<Session>();
            table.Add(session);
            await context.SaveChangesAsync();

            return session;
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
