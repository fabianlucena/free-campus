using FreeCampusServer.Entities;
using FreeCampusServer.QueryOptions;
using RFBaseIServices.IServices;

namespace FreeCampusServer.IServices
{
    public interface ICourseEnrollmentStatusService : ITranslatableEntityService<CourseEnrollmentStatus>
    {
        Task<IEnumerable<CourseEnrollmentStatus>> GetListByOrganizationIdAsync(long organizationId, CourseEnrollmentStatusQueryOptions? options = null);
    }
}
