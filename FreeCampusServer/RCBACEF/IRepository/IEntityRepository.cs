using RCBACEF.Models;
using RCBACEF.QueryOptions;

namespace RCBACEF.IRepository
{
    public interface IEntityRepository<T>
        : IBaseRepository<T>
        where T : Entity, new()
    {
        Task<IEnumerable<Int64>> GetListIdAsync(BaseQueryOptions? options = null);

        Task<T?> GetFirstOrDefaultByUuidAsync(Guid uuid);

        Task<bool> UpdateByIdAsync(Int64 id, Dictionary<string, object> data);
    }
}