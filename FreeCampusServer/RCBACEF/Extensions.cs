using Microsoft.AspNetCore.Builder;
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

            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }

    }
}
