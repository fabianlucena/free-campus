using RCBACEF.Models;

namespace RCBACEF.IServices
{
    public interface IImmutableEntityService<T>
        : ICreatableEntityService<T>
        where T : ImmutableEntity, new()
    {
    }
}