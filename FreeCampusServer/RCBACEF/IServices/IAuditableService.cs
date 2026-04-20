using RCBACEF.Models;

namespace RCBACEF.IServices
{
    public interface IAuditableService<T>
        : ICreatableEntityService<T>
        where T : Auditable, new()
    {
    }
}