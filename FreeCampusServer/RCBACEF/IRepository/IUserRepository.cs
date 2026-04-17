using RCBACEF.Models;
using RCBACEF.QueryOptions;

namespace RCBACEF.IRepository
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetListAsync(UserOptions? options = null);
    }
}
