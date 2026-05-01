using FreeCampusServer.Entities;
using FreeCampusServer.IRepository;
using FreeCampusServer.IServices;
using FreeCampusServer.QueryOptions;
using RFBaseServices.Services;

namespace FreeCampusServer.Service
{
    public class CourseStatusService(ICourseStatusRepository courseStatusRepository)
        : TranslatableEntityService<CourseStatus>(courseStatusRepository),
        ICourseStatusService
    {
        public async Task<IEnumerable<CourseStatus>> GetListByOrganizationIdAsync(long organizationId, CourseStatusQueryOptions? options = null)
            => await courseStatusRepository.GetListByOrganizationIdAsync(organizationId, options);
    }
}
