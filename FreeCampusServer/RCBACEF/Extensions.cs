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
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IRoleIncludeService, RoleIncludeService>();
            services.AddScoped<IRoleXUserService, RoleXUserService>();
            services.AddScoped<IPermissionXRoleService, PermissionXRoleService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IDeviceRepository, DeviceRepository>();
            services.AddScoped<ISessionRepository, SessionRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<IRoleIncludeRepository, RoleIncludeRepository>();
            services.AddScoped<IRoleXUserRepository, RoleXUserRepository>();
            services.AddScoped<IPermissionXRoleRepository, PermissionXRoleRepository>();

            return services;
        }

    }
}
