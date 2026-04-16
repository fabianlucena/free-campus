using RCBACEF.IRepository;
using RCBACEF.IServices;
using RCBACEF.Models;

namespace RCBACEF.Services
{
    public class UserService(IUserRepository userRepository) : IUserService
    {
        public async Task<IEnumerable<User>> GetListAsync()
        {
            return await userRepository.GetListAsync();
        }
    }
}
