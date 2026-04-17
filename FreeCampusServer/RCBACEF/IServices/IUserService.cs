using RCBACEF.Models;
using RCBACEF.QueryOptions;

namespace RCBACEF.IServices
{
    public interface IUserService : ISoftDeletableService<User>
    {
        Task<User> GetSingleByUsernameAsync(string username, UserQueryOptions? options = null);

        string HashPassword(string password);

        bool CheckPassword(User user, string password);
    }
}
