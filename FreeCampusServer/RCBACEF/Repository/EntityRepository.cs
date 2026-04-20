using Microsoft.EntityFrameworkCore;
using RCBACEF.Models;
using RCBACEF.QueryOptions;

namespace RCBACEF.Repository
{
    public class EntityRepository<T>
        : BaseRepository<T>
        where T : Entity, new()
    {
        public EntityRepository(DbContext context) : base(context) { }

        public override IQueryable<T> CreateDBSet(BaseQueryOptions? options = null)
        {
            var quereable = base.CreateDBSet(options);

            return quereable;
        }

        public virtual async Task<IEnumerable<Int64>> GetListIdAsync(BaseQueryOptions? options = null)
        {
            var set = CreateDBSet(options);

            var list = await set
                .Select(e => e.Id)
                .ToListAsync();

            return list;
        }

        public virtual async Task<T> GetSingleByIdAsync(long id, BaseQueryOptions? options = null)
        {
            options ??= new BaseQueryOptions();
            options.Take = 2;
            var set = CreateDBSet(options);
            var list = await set
                .Where(e => e.Id == id)
                .ToListAsync();

            if (list.Count == 0)
            {
                throw new KeyNotFoundException($"Entity with id {id} not found.");

            }
            else if (list.Count > 1)
            {
                throw new InvalidOperationException($"Multiple entities with id {id} found.");
            }

            return list[0];
        }

        public virtual async Task<T?> GetFirstOrDefaultByUuidAsync(Guid uuid)
        {
            var table = context.Set<T>();
            var entity = await table
                .Where(e => e.Uuid == uuid)
                .FirstOrDefaultAsync();

            return entity;
        }

        public virtual async Task<bool> UpdateByIdAsync(Int64 id, Dictionary<string, object> data)
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
