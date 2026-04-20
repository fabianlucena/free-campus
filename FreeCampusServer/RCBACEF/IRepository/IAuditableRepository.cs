using RCBACEF.Models;

namespace RCBACEF.IRepository
{
    public interface IAuditableRepository<T>
        : ICreatableEntityRepository<T>
        where T : Auditable, new()
    {
    }
}