using FreeCampusServer.Entities;
using FreeCampusServer.QueryOptions;
using RFBaseIServices.IServices;

namespace FreeCampusServer.IServices
{
    public interface ICourseService : ICommonEntityService<Course>
    {
        Task<IEnumerable<Course>> GetListByOrganizationIdAsync(long organizationId, CourseQueryOptions? options = null);
    }
}
