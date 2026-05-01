using FreeCampusServer.Entities;
using FreeCampusServer.IRepository;
using FreeCampusServer.IServices;
using FreeCampusServer.QueryOptions;
using RFBaseServices.Services;

namespace FreeCampusServer.Service
{
    public class CourseStatusService(ICourseStatusRepository courseStatusRepository)
        : NominableEntityService<CourseStatus>(courseStatusRepository),
        ICourseStatusService
    {
        public async Task<IEnumerable<CourseStatus>> GetListByOrganizationIdAsync(long organizationId, CourseStatusQueryOptions? options = null)
            => await courseStatusRepository.GetListByOrganizationIdAsync(organizationId, options);
    }
}
