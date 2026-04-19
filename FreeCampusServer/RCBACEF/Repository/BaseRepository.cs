using Microsoft.EntityFrameworkCore;
using RCBACEF.Models;
using RCBACEF.QueryOptions;

namespace RCBACEF.Repository
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
    "Style",
    "IDE0290:Use primary constructor")]
    public class BaseRepository<T> where T : Base, new()
    {
        protected readonly DbContext context;

        public BaseRepository(DbContext context)
        {
            this.context = context;
        }

        public virtual IQueryable<T> CreateDBSet(BaseQueryOptions? options)
        {
            options ??= new BaseQueryOptions();
            IQueryable<T> quereable = context.Set<T>()
                .AsNoTracking();

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

        public virtual async Task<T> CreateAsync(T entity)
        {
            var set = context.Set<T>();
            set.Add(entity);
            await context.SaveChangesAsync();

            return entity;
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

        public async virtual Task<bool> UpdateByIdAsync(Int64 id, Dictionary<string, object> data)
        {
            var entity = new T { Id = id };
            context.Set<T>().Attach(entity);

            foreach (var item in data)
            {
                context.Entry(entity).Property(item.Key).CurrentValue = item.Value;
                context.Entry(entity).Property(item.Key).IsModified = true;
            }

            await context.SaveChangesAsync();

            return true;
        }
    }
}
