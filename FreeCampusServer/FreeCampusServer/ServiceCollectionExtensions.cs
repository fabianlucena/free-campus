using FreeCampusServer.Entities;
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

            services.AddScoped<IProgramTypeService, ProgramTypeService>();
            services.AddScoped<IProgramService, ProgramService>();
            services.AddScoped<IProgramVersionService, ProgramVersionService>();
            services.AddScoped<ICourseTypeService, CourseTypeService>();
            services.AddScoped<ICourseVersionService, CourseVersionService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<ILearningItemTypeService, LearningItemTypeService>();
            services.AddScoped<ILearningItemService, LearningItemService>();
            services.AddScoped<ILearningItemVersionService, LearningItemVersionService>();

            services.AddScoped<IProgramVersionXCourseVersionService, ProgramVersionXCourseVersionService>();
            services.AddScoped<IProgramEnrollmentService, ProgramEnrollmentService>();
            services.AddScoped<ICourseEnrollmentService, CourseEnrollmentService>();

            services.AddScoped<IProgramTypeRepository, ProgramTypeRepository>();
            services.AddScoped<IProgramRepository, ProgramRepository>();
            services.AddScoped<IProgramVersionRepository, ProgramVersionRepository>();
            services.AddScoped<ICourseTypeRepository, CourseTypeRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<ICourseVersionRepository, CourseVersionRepository>();
            services.AddScoped<ILearningItemTypeRepository, LearningItemTypeRepository>();
            services.AddScoped<ILearningItemRepository, LearningItemRepository>();
            services.AddScoped<ILearningItemVersionRepository, LearningItemVersionRepository>();

            services.AddScoped<IProgramVersionXCourseVersionRepository, ProgramVersionXCourseVersionRepository>();
            services.AddScoped<IProgramEnrollmentRepository, ProgramEnrollmentRepository>();
            services.AddScoped<ICourseEnrollmentRepository, CourseEnrollmentRepository>();

            return services;
        }
    }
}
