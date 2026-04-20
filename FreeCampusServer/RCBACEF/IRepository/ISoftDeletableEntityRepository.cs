using RCBACEF.Models;

namespace RCBACEF.IRepository
{
    public interface ISoftDeletableEntityRepository<T>
        : IAuditableEntityRepository<T>
        where T : SoftDeletableEntity, new()
    {
    }
}