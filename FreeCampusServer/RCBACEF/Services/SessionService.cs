using RCBACEF.IRepository;
using RCBACEF.IServices;
using RCBACEF.Models;
using System.Security.Cryptography;

namespace RCBACEF.Services
{
    public class SessionService(ISessionRepository sessionRepository) : BaseService<Session>(sessionRepository), ISessionService
    {
        public int TokenSize { get; set; } = 32;

        public override async Task<Session> ValidateForCreationAsync(Session session)
        {
            session = await base.ValidateForCreationAsync(session);

            if (string.IsNullOrEmpty(session.Token))
            {
                do
                {
                    byte[] bytes = RandomNumberGenerator.GetBytes(TokenSize);
                    var token = Convert.ToBase64String(bytes);
                    session.Token = token;
                } while (await GetFirstOrDefaultByTokenAsync(session.Token) != null);
            } else if (await GetFirstOrDefaultByTokenAsync(session.Token) != null)
            {
                throw new InvalidOperationException("A session with the same token already exists.");
            }

            return session;
        }

        public async Task<Session> CreateAsync(long userId, long deviceId)
        {
            byte[] bytes = RandomNumberGenerator.GetBytes(TokenSize);
            var token = Convert.ToBase64String(bytes);

            var session = new Session
            {
                Token = token,
                ExpireAt = DateTime.MinValue,
                AutoLoginToken = string.Empty,
                UserId = userId,
                User = null,
                DeviceId = deviceId,
                Device = null,
                CreatedById = userId,
            };

            return await CreateAsync(session);
        }
    
        public async Task<Session?> GetFirstOrDefaultByTokenAsync(string token)
        {
            return await sessionRepository.GetFirstOrDefaultByTokenAsync(token);
        }
    }
}
