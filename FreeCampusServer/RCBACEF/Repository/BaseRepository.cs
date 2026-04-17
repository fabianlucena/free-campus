using Microsoft.EntityFrameworkCore;
using RCBACEF.Models;

namespace RCBACEF.Repository
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
    "Style",
    "IDE0290:Use primary constructor")]
    public class BaseRepository<T> where T : Base
    {
        protected readonly DbContext context;

        public BaseRepository(DbContext context)
        {
            this.context = context;
        }

        public virtual async Task<T> ValidateForCreation(T entity)
        {
            if (entity.Id != 0)
            {
                throw new ArgumentException("Entity ID must be zero for new entries.");
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
                throw new ArgumentException("Entity UUID must be empty for new entries.");
            }

            entity.CreatedAt = DateTime.UtcNow;

            var table = context.Set<T>();
            table.Add(entity);
            await context.SaveChangesAsync();

            return entity;
        }

        public async Task<T?> GetFirstOrDefaultByUuidAsync(Guid uuid)
        {
            var table = context.Set<T>();
            var entity = await table
                .Where(e => e.Uuid == uuid)
                .FirstOrDefaultAsync();

            return entity;
        }
    }
}
