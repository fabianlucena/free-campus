using FreeCampusServer.Entities;
using FreeCampusServer.IRepository;
using FreeCampusServer.IServices;
using FreeCampusServer.QueryOptions;
using RFBaseServices.Services;
using RFL10n;

namespace FreeCampusServer.Service
{
    public class CourseTypeService(ICourseTypeRepository courseTypeRepository, IL10n l10n)
        : LocalizableEntityService<CourseType>(courseTypeRepository, l10n),
        ICourseTypeService
    {
        public async Task<IEnumerable<CourseType>> GetListByOrganizationIdAsync(long organizationId, CourseTypeQueryOptions? options = null)
            => await courseTypeRepository.GetListByOrganizationIdAsync(organizationId, options);
    }
}
