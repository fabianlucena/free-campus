using RCBACEF.Models;

namespace RCBACEF.IServices
{
    public interface ICreatableJoinService<T>
        : IJoinService<T>
        where T : CreatableJoin, new()
    {
    }
}
