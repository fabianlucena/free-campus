using RCBACEF.Models;
using RCBACEF.QueryOptions;

namespace RCBACEF.IServices
{
    public interface ICreatableEntityService<T>
        : IEntityService<T>
        where T : CreatableEntity, new()
    {
    }
}
