using RCBACEF.Models;
using RCBACEF.QueryOptions;

namespace RCBACEF.IRepository
{
    public interface IUserRepository : ISoftDeletableRepository<User>
    {
        Task<User> GetSingleByUsernameAsync(string username, UserQueryOptions? options = null);
        Task<User?> GetSingleOrDefaultByUsernameAsync(string username, UserQueryOptions? options = null);
    }
}
