using RCBACEF.Models;
using RCBACEF.QueryOptions;

namespace RCBACEF.IRepository
{
    public interface ISessionRepository : ICreatableEntityRepository<Session>
    {
        Task<Session?> GetFirstOrDefaultByTokenAsync(string token, SessionQueryOptions? options = null);
    }
}