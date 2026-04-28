using FreeCampusServer.IRepository;
using FreeCampusServer.IServices;
using FreeCampusServer.Repository;
using FreeCampusServer.Service;

namespace FreeCampusServer
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddFCServices(this IServiceCollection services)
        {
            services.AddScoped<ICourseService, CourseService>();

            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<ICourseTypeRepository, CourseTypeRepository>();

            return services;
        }
    }
}
