using RCBACEF.Models;

namespace RCBACEF.IRepository
{
    public interface IAuditableEntityRepository<T>
        : ICreatableEntityRepository<T>
        where T : AuditableEntity, new()
    {
    }
}