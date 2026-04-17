using RCBACEF.DTO;
using RCBACEF.IServices;
using RCBACEF.Models;

namespace RCBACEF.Services
{
    public class LoginService(IUserService userService, ISessionService sessionService, IDeviceService deviceService) : ILoginService
    {
        public async Task<Session> LoginAsync(LoginRequest request)
        {
            var user = await userService.GetSingleByUsernameAsync(request.Username)
                ?? throw new Exception("User not found.");

            if (user.DeletedAt.HasValue)
            {
                throw new Exception("User is deleted.");
            }

            if (!user.IsActive)
            {
                throw new Exception("User is not active.");
            }

            if (!user.CanLogin)
            {
                throw new Exception("User is not allowed to login.");
            }

            if (!userService.CheckPassword(user, request.Password))
            {
                throw new Exception("Invalid password.");
            }

            var device = await deviceService.GetFirstOrCreateByTokenAsync(request.DeviceToken);

            var session = await sessionService.CreateAsync(user.Id, device.Id);

            return session;
        }
    }
}
