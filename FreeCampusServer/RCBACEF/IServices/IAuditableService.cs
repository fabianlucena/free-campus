using RCBACEF.Models;

namespace RCBACEF.IServices
{
    public interface IAuditableService<T> : IBaseService<T> where T : Auditable
    {
    }
}