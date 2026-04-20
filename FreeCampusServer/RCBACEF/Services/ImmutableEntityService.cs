using RCBACEF.IRepository;
using RCBACEF.IServices;
using RCBACEF.Models;

namespace RCBACEF.Services
{
    public class ImmutableEntityService<T>(IImmutableEntityRepository<T> repository)
        : CreatableEntityService<T>(repository),
        IImmutableEntityService<T>
        where T : ImmutableEntity, new()
    {
    }
}
