using RCBACEF.Models;

namespace RCBACEF.IServices
{
    public interface ISoftDeletableJoinService<T>
        : ICreatableJoinService<T>
        where T : SoftDeletableJoin, new()
    {
    }
}