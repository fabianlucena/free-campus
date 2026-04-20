using RCBACEF.Models;
using RCBACEF.QueryOptions;

namespace RCBACEF.IRepository
{
    public interface ICreatableEntityRepository<T>
        : IEntityRepository<T>
        where T : CreatableEntity, new()
    {
    }
}