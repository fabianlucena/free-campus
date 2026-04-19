using RCBACEF.Models;
using RCBACEF.QueryOptions;

namespace RCBACEF.IServices
{
    public interface ISessionService : IBaseService<Session>
    {
        Task<Session> CreateAsync(Int64 userId, Int64 deviceId);
        Task<Session?> GetFirstOrDefaultByTokenAsync(string token, SessionQueryOptions? options = null);
        Task UpdateLastUsageAsync(Int64 sessionId);
    }
}
