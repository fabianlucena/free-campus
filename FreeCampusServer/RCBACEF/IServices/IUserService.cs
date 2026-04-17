using RCBACEF.Models;
using RCBACEF.QueryOptions;

namespace RCBACEF.IServices
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetListAsync(UserQueryOptions? options = null);

        Task<User> GetSingleByUsernameAsync(string username, UserQueryOptions? options = null);

        bool CheckPassword(User user, string password);
    }
}
