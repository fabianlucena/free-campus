using RCBACEF.Models;

namespace RCBACEF.IRepository
{
    public interface IImmutableRepository<T> : IBaseRepository<T> where T : Immutable
    {
    }
}