using FreeCampusServer.Entities;
using FreeCampusServer.QueryOptions;
using RFBaseIServices.IServices;

namespace FreeCampusServer.IServices
{
    public interface ICourseTypeService : ICommonEntityService<CourseType>
    {
        Task<IEnumerable<CourseType>> GetListByOrganizationIdAsync(long organizationId, CourseTypeQueryOptions? options = null);
    }
}
