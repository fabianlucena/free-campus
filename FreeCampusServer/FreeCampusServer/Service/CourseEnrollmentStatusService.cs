using FreeCampusServer.Entities;
using FreeCampusServer.IRepository;
using FreeCampusServer.IServices;
using FreeCampusServer.QueryOptions;
using RFBaseServices.Services;

namespace FreeCampusServer.Service
{
    public class CourseEnrollmentStatusService(ICourseEnrollmentStatusRepository courseEnrollmentStatusRepository)
        : TranslatableEntityService<CourseEnrollmentStatus>(courseEnrollmentStatusRepository),
        ICourseEnrollmentStatusService
    {
        public async Task<IEnumerable<CourseEnrollmentStatus>> GetListByOrganizationIdAsync(long organizationId, CourseEnrollmentStatusQueryOptions? options = null)
             => await courseEnrollmentStatusRepository.GetListAsync(new CourseEnrollmentStatusQueryOptions(options)
            {
                OrganizationId = organizationId,
            });
    }
}
