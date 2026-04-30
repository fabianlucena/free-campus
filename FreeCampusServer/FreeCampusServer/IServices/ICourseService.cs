using FreeCampusServer.Entities;
using FreeCampusServer.Exceptions;
using FreeCampusServer.QueryOptions;
using FreeCampusServer.Repository;
using RFBaseIServices.IServices;

namespace FreeCampusServer.IServices
{
    public interface ICourseService : ICommonEntityService<Course>
    {
        Task<IEnumerable<Course>> GetListAsync(CourseQueryOptions options);
        Task<IEnumerable<Course>> GetStandaloneListAsync(CourseQueryOptions options);
        Task<IEnumerable<Course>> GetAvailableListAsync(CourseQueryOptions options);
    }
}
