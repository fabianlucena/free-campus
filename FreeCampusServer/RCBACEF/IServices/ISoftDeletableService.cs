using RCBACEF.Models;

namespace RCBACEF.IServices
{
    public interface ISoftDeletableService<T>
        : IAuditableEntityService<T>
        where T : SoftDeletable, new()
    {
    }
}