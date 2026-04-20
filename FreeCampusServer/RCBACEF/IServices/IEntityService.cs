using RCBACEF.Models;
using RCBACEF.QueryOptions;

namespace RCBACEF.IServices
{
    public interface IEntityService<T>
        : IBaseService<T>
        where T : Entity
    {
        Task<IEnumerable<long>> GetListIdAsync(BaseQueryOptions? options = null);
        Task<T> GetSingleByIdAsync(long id, BaseQueryOptions? options = null);
        Task UpdateByIdAsync(long id, Dictionary<string, object> data);
    }
}
