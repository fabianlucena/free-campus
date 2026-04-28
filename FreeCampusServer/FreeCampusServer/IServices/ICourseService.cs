using FreeCampusServer.Entities;
using FreeCampusServer.QueryOptions;
using RFBaseIServices.IServices;

namespace FreeCampusServer.IServices
{
    public interface ICourseService : ICommonEntityService<Course>
    {
        Task<IEnumerable<Course>> GetListByStandaloneAsync(CourseQueryOptions? options = null);
    }
}
