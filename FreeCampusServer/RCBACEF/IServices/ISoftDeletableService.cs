using RCBACEF.Models;

namespace RCBACEF.IServices
{
    public interface ISoftDeletableService<T>
        : IAuditableService<T>
        where T : SoftDeletable, new()
    {
    }
}