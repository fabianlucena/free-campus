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
            services.AddScoped<IProgramService, ProgramService>();
            services.AddScoped<IProgramTypeService, ProgramTypeService>();
            services.AddScoped<IModuleService, ModuleService>();
            services.AddScoped<IModuleTypeService, ModuleTypeService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<ICourseTypeService, CourseTypeService>();

            services.AddScoped<IProgramRepository, ProgramRepository>();
            services.AddScoped<IProgramTypeRepository, ProgramTypeRepository>();
            services.AddScoped<IModuleRepository, ModuleRepository>();
            services.AddScoped<IModuleTypeRepository, ModuleTypeRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<ICourseTypeRepository, CourseTypeRepository>();

            return services;
        }
    }
}
