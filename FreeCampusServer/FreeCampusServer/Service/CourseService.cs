using FreeCampusServer.Entities;
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
        public async Task<IEnumerable<Course>> GetListAvailableByOrganizationIdAsync(long organizationId, CourseQueryOptions? options = null)
        {
            var programService = serviceProvider.GetRequiredService<IProgramService>();

            var list = await courseRepository.GetStandaloneListByOrganizationIdAsync(organizationId, options);
            list = list.Where(c => c.IsStandalone);

            var programList = await programService.GetCoursesByOrganizationIdAsync(organizationId);

            return list;
        }
    }
}
