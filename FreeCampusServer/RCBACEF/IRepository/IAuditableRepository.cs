using RCBACEF.Models;

namespace RCBACEF.IRepository
{
    public interface IAuditableRepository<T> : IBaseRepository<T> where T : Auditable
    {
    }
}