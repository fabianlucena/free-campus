using RCBACEF.IRepository;
using RCBACEF.IServices;
using RCBACEF.Models;
using System.Security.Cryptography;

namespace RCBACEF.Services
{
    public class SessionService(ISessionRepository sessionRepository) : ISessionService
    {
        public int TokenSize { get; set; } = 32;

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
                Device = null
            };

            return await sessionRepository.CreateAsync(session);
        }
    }
}
