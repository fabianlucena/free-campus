using RCBACEF.Models;

namespace RCBACEF.IRepository
{
    public interface ISessionRepository : IBaseRepository<Session>
    {
        Task<Session?> GetFirstOrDefaultByTokenAsync(string token);
    }
}