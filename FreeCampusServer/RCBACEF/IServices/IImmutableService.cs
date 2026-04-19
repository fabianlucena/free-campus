using RCBACEF.Models;

namespace RCBACEF.IServices
{
    public interface IImmutableService<T> : IBaseService<T> where T : Immutable
    {
    }
}