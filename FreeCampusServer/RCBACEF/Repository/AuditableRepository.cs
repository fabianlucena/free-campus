using Microsoft.EntityFrameworkCore;
using RCBACEF.Models;

namespace RCBACEF.Repository
{
    public class AuditableRepository<T> : BaseRepository<T> where T : Auditable
    {
        public AuditableRepository(DbContext context) : base(context)
        {
        }

        public override async Task<T> ValidateForCreation(T entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;

            return entity;
        }
    }
}
