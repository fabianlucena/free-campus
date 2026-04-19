using RCBACEF.Models;
using RCBACEF.QueryOptions;

namespace RCBACEF.IServices
{
    public interface IBaseService<T> where T : Base
    {
        Task<T> CreateAsync(T entity);
        Task<IEnumerable<T>> GetListAsync(BaseQueryOptions? options = null);
        Task UpdateByIdAsync(Int64 id, Dictionary<string, object> data);
    }
}
