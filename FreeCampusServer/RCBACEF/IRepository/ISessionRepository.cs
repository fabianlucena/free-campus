using RCBACEF.Models;

namespace RCBACEF.IRepository
{
    public interface ISessionRepository
    {
        Task<Session> CreateAsync(Session session);
    }
}