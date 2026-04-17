using RCBACEF.DTO;
using RCBACEF.Models;

namespace RCBACEF.IServices
{
    public interface ILoginService
    {
        Task<Session> LoginAsync(LoginRequest request);
    }
}
