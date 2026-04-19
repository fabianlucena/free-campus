using RCBACEF.Models;
using RCBACEF.QueryOptions;

namespace RCBACEF.IRepository
{
    public interface ISessionRepository : IBaseRepository<Session>
    {
        Task<Session?> GetFirstOrDefaultByTokenAsync(string token, SessionQueryOptions? options = null);
    }
}