using FreeCampusServer.Entities;
using FreeCampusServer.IRepository;
using FreeCampusServer.IServices;
using FreeCampusServer.QueryOptions;
using RFBaseServices.Services;

namespace FreeCampusServer.Service
{
    public class CourseTypeService(ICourseTypeRepository courseTypeRepository)
        : LocalizableEntityService<CourseType>(courseTypeRepository),
        ICourseTypeService
    {
        public async Task<IEnumerable<CourseType>> GetListByOrganizationIdAsync(long organizationId, CourseTypeQueryOptions? options = null)
            => await courseTypeRepository.GetListByOrganizationIdAsync(organizationId, options);
    }
}
