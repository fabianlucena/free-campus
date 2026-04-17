using RCBACEF.Models;
using RCBACEF.QueryOptions;

namespace RCBACEF.IRepository
{
    public interface IBaseRepository<T> where T : Base
    {
        Task<T> CreateAsync(T entity);

        Task<IEnumerable<T>> GetListAsync(BaseQueryOptions? options = null);

        Task<T?> GetFirstOrDefaultByUuidAsync(Guid uuid);
    }
}