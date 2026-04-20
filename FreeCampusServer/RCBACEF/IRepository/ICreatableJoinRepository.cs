using RCBACEF.Models;

namespace RCBACEF.IRepository
{
    public interface ICreatableJoinRepository<T>
        : IJoinRepository<T>
        where T : CreatableJoin, new()
    {
    }
}