using RCBACEF.Models;

namespace RCBACEF.IRepository
{
    public interface IJoinRepository<T>
        : IBaseRepository<T>
        where T : Join, new()
    {
    }
}