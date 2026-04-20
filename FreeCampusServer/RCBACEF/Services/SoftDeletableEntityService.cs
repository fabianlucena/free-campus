using RCBACEF.IRepository;
using RCBACEF.IServices;
using RCBACEF.Models;

namespace RCBACEF.Services
{
    public class SoftDeletableEntityService<T>(ISoftDeletableEntityRepository<T> repository)
        : AuditableEntityService<T>(repository),
        ISoftDeletableEntityService<T>
        where T : SoftDeletableEntity, new()
    {
    }
}
