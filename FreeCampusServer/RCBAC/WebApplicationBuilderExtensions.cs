using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using RCBAC.IRepository;
using RCBAC.IServices;
using RCBAC.Repository;
using RCBAC.Services;

namespace RCBAC
{
    public static class WebApplicationBuilderExtensions
    {
        public static WebApplicationBuilder AddRCBAC(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddScoped<IUserRepository, UserRepository>();

            return builder;
        }

    }
}
