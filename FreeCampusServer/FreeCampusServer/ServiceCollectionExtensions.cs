using FreeCampusServer.IRepository;
using FreeCampusServer.IServices;
using FreeCampusServer.Repository;
using FreeCampusServer.Service;
using RFL10n;
using RFRGOBACIServices.IServices;

namespace FreeCampusServer
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddFCServices(this IServiceCollection services)
        {
            services.Decorate<IOrganizationService, OrganizationServiceDecorator>();

            services.AddScoped<IProgramService, ProgramService>();
            services.AddScoped<IProgramTypeService, ProgramTypeService>();
            services.AddScoped<IModuleService, ModuleService>();
            services.AddScoped<IModuleTypeService, ModuleTypeService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<ICourseTypeService, CourseTypeService>();

            services.AddScoped<ICourseXProgramService, CourseXProgramService>();
            services.AddScoped<IProgramEnrollmentService, ProgramEnrollmentService>();
            services.AddScoped<ICourseEnrollmentService, CourseEnrollmentService>();

            services.AddScoped<IProgramRepository, ProgramRepository>();
            services.AddScoped<IProgramTypeRepository, ProgramTypeRepository>();
            services.AddScoped<IModuleRepository, ModuleRepository>();
            services.AddScoped<IModuleTypeRepository, ModuleTypeRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<ICourseTypeRepository, CourseTypeRepository>();

            services.AddScoped<ICourseXProgramRepository, CourseXProgramRepository>();
            services.AddScoped<IProgramEnrollmentRepository, ProgramEnrollmentRepository>();
            services.AddScoped<ICourseEnrollmentRepository, CourseEnrollmentRepository>();

            return services;
        }
    }
}
