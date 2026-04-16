using RCBACEF.IServices;
using RCBACEF.Models;

namespace RCBACEF.Services
{
    public class UserService : IUserService
    {
        public async Task<IEnumerable<User>> GetListAsync()
        {
            return await Task.FromResult(Enumerable.Range(1, 5).Select(index => new User
            {
                Uuid = Guid.NewGuid(),
                Username = $"User{index}",
                DisplayName = $"User {index}"
            }));
        }
    }
}
