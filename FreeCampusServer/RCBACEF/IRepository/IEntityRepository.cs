using RCBACEF.Models;
using RCBACEF.QueryOptions;

namespace RCBACEF.IRepository
{
    public interface IEntityRepository<T>
        : IBaseRepository<T>
        where T : Entity, new()
    {
        Task<IEnumerable<long>> GetListIdAsync(BaseQueryOptions? options = null);

        Task<T> GetSingleByIdAsync(long id, BaseQueryOptions? options = null);

        Task<T?> GetFirstOrDefaultByUuidAsync(Guid uuid);

        Task<bool> UpdateByIdAsync(long id, Dictionary<string, object> data);
    }
}