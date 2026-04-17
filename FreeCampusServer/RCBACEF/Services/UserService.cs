using RCBACEF.IRepository;
using RCBACEF.IServices;
using RCBACEF.Models;
using RCBACEF.QueryOptions;

namespace RCBACEF.Services
{
    public class UserService(IUserRepository userRepository) : IUserService
    {
        public async Task<IEnumerable<User>> GetListAsync(UserOptions? options = null)
        {
            return await userRepository.GetListAsync(options);
        }
    }
}
