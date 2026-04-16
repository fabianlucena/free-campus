using RCBACEF.Models;

namespace RCBACEF.IRepository
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetListAsync();
    }
}
