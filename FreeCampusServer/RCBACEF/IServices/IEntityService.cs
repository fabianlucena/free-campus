using RCBACEF.Models;
using RCBACEF.QueryOptions;

namespace RCBACEF.IServices
{
    public interface IEntityService<T>
        : IBaseService<T>
        where T : Entity
    {
        Task<IEnumerable<Int64>> GetListIdAsync(BaseQueryOptions? options = null);
        Task UpdateByIdAsync(Int64 id, Dictionary<string, object> data);
    }
}
