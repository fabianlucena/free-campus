using RCBACEF.Models;
using RCBACEF.QueryOptions;

namespace RCBACEF.IServices
{
    public interface IUserService : ISoftDeletableEntityService<User>
    {
        string HashPassword(string password);
        bool CheckPassword(User user, string password);

        Task<User> GetSingleByUsernameAsync(string username, UserQueryOptions? options = null);

        Task<User?> GetSingleOrDefaultByUsernameAsync(string username, UserQueryOptions? options = null);

        Task<User> GetSystemUserAsync();

        Task<User> GetCurrentOrSystemUserAsync();

        Task<Int64> GetCurrentOrSystemUserIdAsync();

        Task UpdateLastLoginAsync(Int64 userId);
    }
}
