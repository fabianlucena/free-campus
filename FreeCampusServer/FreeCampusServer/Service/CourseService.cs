using FreeCampusServer.Entities;
using FreeCampusServer.IRepository;
using FreeCampusServer.IServices;
using FreeCampusServer.QueryOptions;
using RFBaseServices.Services;

namespace FreeCampusServer.Service
{
    public class CourseService(ICourseRepository courseRepository)
        : CommonEntityService<Course>(courseRepository),
        ICourseService
    {
        public async Task<IEnumerable<Course>> GetListByStandaloneAsync(CourseQueryOptions? options = null)
        {
            return await courseRepository.GetListByStandaloneAsync(options);
        }
    }
}
