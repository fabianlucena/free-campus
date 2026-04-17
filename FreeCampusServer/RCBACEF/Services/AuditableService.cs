using RCBACEF.IRepository;
using RCBACEF.IServices;
using RCBACEF.Models;

namespace RCBACEF.Services
{
    public class AuditableService<T>(IAuditableRepository<T> repository) : BaseService<T>(repository), IAuditableService<T> where T : Auditable
    {
        public override async Task<T> ValidateForCreationAsync(T entity)
        {
            if (entity.UpdatedById == 0)
            {
                throw new ArgumentException("UpdatedById must be set for new entries.");
            }

            entity.UpdatedAt = DateTime.UtcNow;

            return entity;
        }
    }
}
