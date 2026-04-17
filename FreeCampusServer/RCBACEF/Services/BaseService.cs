using RCBACEF.IRepository;
using RCBACEF.IServices;
using RCBACEF.Models;
using RCBACEF.QueryOptions;

namespace RCBACEF.Services
{
    public class BaseService<T>(IBaseRepository<T> repository) : IBaseService<T> where T : Base
    {
        public virtual async Task<T> ValidateForCreationAsync(T entity)
        {
            if (entity.Id != 0)
            {
                throw new ArgumentException("Entity ID must be zero for new entries.");
            }

            if (entity.CreatedById == 0)
            {
                throw new ArgumentException("CreatedById must be set for new entries.");
            }

            if (entity.Uuid == Guid.Empty)
            {
                do
                {
                    entity.Uuid = Guid.NewGuid();
                } while (await GetFirstOrDefaultByUuidAsync(entity.Uuid) != null);
            }
            else
            {
                throw new ArgumentException("An entity for the provided UUID already exists.");
            }

            entity.CreatedAt = DateTime.UtcNow;

            return entity;
        }

        public async Task<T> CreateAsync(T entity)
        {
            entity = await ValidateForCreationAsync(entity);
            return await repository.CreateAsync(entity);
        }

        public async Task<IEnumerable<T>> GetListAsync(BaseQueryOptions? options = null)
        {
            return await repository.GetListAsync(options);
        }

        public async Task<T?> GetFirstOrDefaultByUuidAsync(Guid uuid)
        {
            return await repository.GetFirstOrDefaultByUuidAsync(uuid);
        }
    }
}
