using FreeCampusServer.Entities;
using FreeCampusServer.QueryOptions;
using RFBaseIRepositories.IRepositories;

namespace FreeCampusServer.IRepository
{
    public interface ICourseTypeRepository : ICommonEntityRepository<CourseType>
    {
        Task<IEnumerable<CourseType>> GetListByOrganizationIdAsync(long organizationId, CourseTypeQueryOptions? options = null);
    }
}