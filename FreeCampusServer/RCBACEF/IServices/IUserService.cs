using RCBACEF.Models;
using RCBACEF.QueryOptions;

namespace RCBACEF.IServices
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetListAsync(UserOptions? options = null);
    }
}
