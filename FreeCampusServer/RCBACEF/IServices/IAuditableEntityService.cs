using RCBACEF.Models;

namespace RCBACEF.IServices
{
    public interface IAuditableEntityService<T>
        : ICreatableEntityService<T>
        where T : AuditableEntity, new()
    {
    }
}