using RCBACEF.Models;

namespace RCBACEF.IServices
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetListAsync();
    }
}
