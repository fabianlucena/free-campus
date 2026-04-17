using RCBACEF.Models;
using RCBACEF.QueryOptions;

namespace RCBACEF.IRepository
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetListAsync(UserQueryOptions? options = null);

        Task<User> GetSingleByUsernameAsync(string username, UserQueryOptions? options = null);
    }
}
