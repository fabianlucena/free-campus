using RCBACEF.IRepository;
using RCBACEF.IServices;
using RCBACEF.Models;

namespace RCBACEF.Services
{
    public class AuditableService<T>(IAuditableRepository<T> repository)
        : CreatableEntityService<T>(repository),
        IAuditableService<T>
        where T : Auditable, new()
    {
        public override async Task<T> ValidateForCreateAsync(T entity)
        {
            entity = await base.ValidateForCreateAsync(entity);

            if (entity.UpdatedById <= 0)
            {
                throw new ArgumentException("UpdatedById must be set for auditable entries.");
            }

            entity.UpdatedAt = DateTime.UtcNow;

            return entity;
        }

        public override async Task<Dictionary<string, object>> ValidateForUpdate(Dictionary<string, object> data)
        {
            data = await base.ValidateForUpdate(data);

            if (!data.TryGetValue("UpdatedById", out object? value) || (Int64)value <= 0)
            {
                throw new InvalidOperationException("UpdatedById must be set for auditable entities.");
            }

            data["UpdatedAt"] = DateTime.UtcNow;

            return data;
        }
    }
}
