using RCBACEF.Models;
using RCBACEF.QueryOptions;

namespace RCBACEF.IServices
{
    public interface ISessionService : ICreatableEntityService<Session>
    {
        Task<Session> CreateAsync(long userId, long deviceId, long? companyId = null);
        Task<Session?> GetFirstOrDefaultByTokenAsync(string token, SessionQueryOptions? options = null);
        Task UpdateLastUsageAsync(long sessionId);
    }
}
