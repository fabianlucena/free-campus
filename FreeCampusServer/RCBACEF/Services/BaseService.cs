using RCBACEF.IRepository;
using RCBACEF.IServices;
using RCBACEF.Models;
using RCBACEF.QueryOptions;

namespace RCBACEF.Services
{
    public class BaseService<T>(IBaseRepository<T> repository)
        : IBaseService<T>
        where T : Base
    {
        public virtual async Task<T> ValidateForCreateAsync(T entity)
        {
            return entity;
        }

        public virtual async Task<Dictionary<string, object>> ValidateForUpdate(Dictionary<string, object> data)
        {
            return data;
        }

        public async Task<T> CreateAsync(T entity)
        {
            entity = await ValidateForCreateAsync(entity);
            return await repository.CreateAsync(entity);
        }

        public async Task<IEnumerable<T>> GetListAsync(BaseQueryOptions? options = null)
        {
            return await repository.GetListAsync(options);
        }
    }
}
