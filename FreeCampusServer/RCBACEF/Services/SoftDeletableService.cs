using RCBACEF.IRepository;
using RCBACEF.IServices;
using RCBACEF.Models;

namespace RCBACEF.Services
{
    public class SoftDeletableService<T>(ISoftDeletableRepository<T> repository) : AuditableService<T>(repository), ISoftDeletableService<T> where T : SoftDeletable
    {
    }
}
