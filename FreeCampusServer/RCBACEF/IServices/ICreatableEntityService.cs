using RCBACEF.Models;

namespace RCBACEF.IServices
{
    public interface ICreatableEntityService<T>
        : IEntityService<T>
        where T : CreatableEntity, new()
    {
    }
}
