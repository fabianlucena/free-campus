using RCBACEF.DTO;
using RCBACEF.Models;

namespace RCBACEF.IServices
{
    public interface ISessionService : IBaseService<Session>
    {
        Task<Session> CreateAsync(Int64 userId, Int64 deviceId);
        Task<Session?> GetFirstOrDefaultByTokenAsync(string token);
    }
}
