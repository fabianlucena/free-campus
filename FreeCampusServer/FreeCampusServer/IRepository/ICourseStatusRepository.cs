using FreeCampusServer.Entities;
using FreeCampusServer.QueryOptions;
using RFBaseIRepositories.IRepositories;

namespace FreeCampusServer.IRepository
{
    public interface ICourseStatusRepository : INominableEntityRepository<CourseStatus>
    {
        Task<IEnumerable<CourseStatus>> GetListByOrganizationIdAsync(long organizationId, CourseStatusQueryOptions? options = null);
    }
}