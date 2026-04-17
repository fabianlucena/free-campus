using Microsoft.Extensions.DependencyInjection;
using RCBACEF.IRepository;
using RCBACEF.IServices;
using RCBACEF.Repository;
using RCBACEF.Services;

namespace RCBACEF
{
    public static class Extensions
    {
        public static IServiceCollection AddRCBACEF(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IDeviceService, DeviceService>();
            services.AddScoped<ISessionService, SessionService>();
            services.AddScoped<ILoginService, LoginService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IDeviceRepository, DeviceRepository>();
            services.AddScoped<ISessionRepository, SessionRepository>();

            return services;
        }

    }
}
