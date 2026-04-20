using RCBACEF.Models;

namespace RCBACEF.IRepository
{
    public interface ISoftDeletableRepository<T>
        : IAuditableEntityRepository<T>
        where T : SoftDeletable, new()
    {
    }
}