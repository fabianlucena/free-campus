using Microsoft.EntityFrameworkCore;
using RCBACEF.Models;
using RCBACEF.QueryOptions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
            entity.CreatedAt = DateTime.UtcNow;

            return entity;
        }

        public virtual async Task<T> CreateAsync(T entity)
        {
            var table = context.Set<T>();
            table.Add(entity);
            await context.SaveChangesAsync();

            return entity;
        }

        public virtual IQueryable<T> CreateDBSet(BaseQueryOptions options)
        {
            IQueryable<T> quereable = context.Set<T>();

            if (options is BaseQueryOptions baseOptions)
            {
                if (baseOptions.IncludeCreatedBy)
                {
                    quereable = quereable.Include(u => u.CreatedBy);
                }

                quereable = quereable.Take(options.Take);
            }

            return quereable;
        }

        public virtual async Task<IEnumerable<T>> GetListAsync(BaseQueryOptions? options)
        {
            var set = CreateDBSet(options);

            var list = await set
                .ToListAsync();

            return list;
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
