using FreeCampusServer.Entities;
using FreeCampusServer.Exceptions;
using FreeCampusServer.IRepository;
using FreeCampusServer.IServices;
using FreeCampusServer.QueryOptions;
using RFBaseServices.Services;

namespace FreeCampusServer.Service
{
    public class CourseService(
        ICourseRepository courseRepository,
        IServiceProvider serviceProvider
    )
        : CommonEntityService<Course>(courseRepository),
        ICourseService
    {
        public async Task<IEnumerable<Course>> GetAvailableListAsync(CourseQueryOptions options)
        {
            if (options.OrganizationId is null)
                throw new NoOrganizationIdException();

            if (options.StudentId is null)
                throw new NoStudentIdException();

            var standalonelist = await GetListAsync(new CourseQueryOptions(options)
            {
                IsStandalone = true,
                StudentId = null,
                ExcludeStudentId = options.StudentId,
            });

            var courseXProgramService = serviceProvider.GetRequiredService<ICourseXProgramService>();
            var programCourses = await courseXProgramService.GetCoursesAsync(new CourseXProgramQueryOptions
            {
                OrganizationId = options.OrganizationId,
                ExcludeStudentId = options.ExcludeStudentId,
            });

            return standalonelist
                .Concat(programCourses);
        }
    }
}
