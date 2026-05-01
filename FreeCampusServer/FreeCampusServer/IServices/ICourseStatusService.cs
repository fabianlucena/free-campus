using FreeCampusServer.Entities;
using FreeCampusServer.QueryOptions;
using RFBaseIServices.IServices;

namespace FreeCampusServer.IServices
{
    public interface ICourseStatusService : INominableEntityService<CourseStatus>
    {
        Task<IEnumerable<CourseStatus>> GetListByOrganizationIdAsync(long organizationId, CourseStatusQueryOptions? options = null);
    }
}
