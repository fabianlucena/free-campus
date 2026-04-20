using RCBACEF.Models;

namespace RCBACEF.IServices
{
    public interface IJoinService<T>
        : IBaseService<T>
        where T : Join, new()
    {
    }
}
