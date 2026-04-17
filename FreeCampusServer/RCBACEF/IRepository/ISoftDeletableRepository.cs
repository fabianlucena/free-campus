using RCBACEF.Models;

namespace RCBACEF.IRepository
{
    public interface ISoftDeletableRepository<T> : IAuditableRepository<T> where T : SoftDeletable
    {
    }
}