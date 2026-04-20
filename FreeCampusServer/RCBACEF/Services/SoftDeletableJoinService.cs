using RCBACEF.IRepository;
using RCBACEF.IServices;
using RCBACEF.Models;

namespace RCBACEF.Services
{
    public class SoftDeletableJoinService<T>(ISoftDeletableJoinRepository<T> repository)
        : CreatableJoinService<T>(repository),
        ISoftDeletableJoinService<T>
        where T : SoftDeletableJoin, new()
    {
    }
}
