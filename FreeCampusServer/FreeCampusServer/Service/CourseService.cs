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
        public async Task<IEnumerable<Course>> GetListAsync(CourseQueryOptions options)
        {
            if (options.OrganizationId is null)
                throw new NoOrganizationIdException();

            return await courseRepository.GetListAsync(options);
        }

        public async Task<IEnumerable<Course>> GetStandaloneListAsync(CourseQueryOptions options)
        {
            options = new CourseQueryOptions(options)
            {
                Standalone = true
            };

            return await GetListAsync(options);
        }

        public async Task<IEnumerable<Course>> GetAvailableListAsync(CourseQueryOptions options)
        {
            var programService = serviceProvider.GetRequiredService<IProgramService>();

            var standalonelist = await GetStandaloneListAsync(options);
            var programList = await programService.GetAvailableCoursesAsync(new ProgramQueryOptions {
                OrganizationId = options.OrganizationId,
                StudentId = options.StudentId,
            });

            return standalonelist
                .Concat(programList);
        }
    }
}
