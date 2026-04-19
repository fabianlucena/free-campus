using RCBACEF.IRepository;
using RCBACEF.IServices;
using RCBACEF.Models;

namespace RCBACEF.Services
{
    public class ImmutableService<T>(IImmutableRepository<T> repository) : BaseService<T>(repository), IImmutableService<T> where T : Immutable
    {
    }
}
