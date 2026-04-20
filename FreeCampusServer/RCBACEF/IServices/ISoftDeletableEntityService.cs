using RCBACEF.Models;

namespace RCBACEF.IServices
{
    public interface ISoftDeletableEntityService<T>
        : IAuditableEntityService<T>
        where T : SoftDeletableEntity, new()
    {
    }
}