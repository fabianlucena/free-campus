using RCBACEF.Models;

namespace RCBACEF.IRepository
{
    public interface ISoftDeletableJoinRepository<T>
        : ICreatableJoinRepository<T>
        where T : SoftDeletableJoin, new()
    {
    }
}