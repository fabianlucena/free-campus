using RCBACEF.DTO;
using RCBACEF.Models;

namespace RCBACEF.IServices
{
    public interface ISessionService
    {
        Task<Session> CreateAsync(Int64 userId, Int64 deviceId);
    }
}
