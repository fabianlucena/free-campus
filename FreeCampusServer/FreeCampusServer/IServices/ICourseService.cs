using FreeCampusServer.Entities;
using FreeCampusServer.QueryOptions;
using RFBaseIServices.IServices;

namespace FreeCampusServer.IServices
{
    public interface ICourseService : ICommonEntityService<Course>
    {
        Task<IEnumerable<Course>> GetAvailableListAsync(CourseQueryOptions options);
        Task<IEnumerable<Course>> GetMineListAsync(CourseQueryOptions options);
    }
}
