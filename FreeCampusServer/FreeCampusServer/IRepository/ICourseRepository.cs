using FreeCampusServer.Entities;
using FreeCampusServer.QueryOptions;
using RFBaseIRepositories.IRepositories;

namespace FreeCampusServer.IRepository
{
    public interface ICourseRepository : ICommonEntityRepository<Course>
    {
        Task<IEnumerable<Course>> GetListByStandaloneAsync(CourseQueryOptions? options = null);
    }
}