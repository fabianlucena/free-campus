using FreeCampusServer.Entities;
using FreeCampusServer.QueryOptions;
using RFBaseIServices.IServices;

namespace FreeCampusServer.IServices
{
    public interface IRepositoryEnrollmentService : ICommonEntityService<CourseEnrollment>
    {
        Task<IEnumerable<long>> GetCourseIdsAsync(CourseEnrollmentQueryOptions options);
    }
}
