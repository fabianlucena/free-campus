using RCBACEF.Models;

namespace RCBACEF.IRepository
{
    public interface IImmutableEntityRepository<T>
        : ICreatableEntityRepository<T>
        where T : ImmutableEntity, new()
    {
    }
}