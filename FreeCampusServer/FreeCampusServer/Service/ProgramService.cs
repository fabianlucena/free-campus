using FreeCampusServer.Entities;
using FreeCampusServer.IRepository;
using FreeCampusServer.IServices;
using FreeCampusServer.QueryOptions;
using FreeCampusServer.Repository;
using RFBaseServices.Services;

namespace FreeCampusServer.Service
{
    public class ProgramService(
        IProgramRepository programRepository,
        IServiceProvider serviceProvider
    )
        : CommonEntityService<Entities.Program>(programRepository),
        IProgramService
    {
        public async Task<IEnumerable<long>> GetIdListByOrganizationIdAsync(long organizationId, ProgramQueryOptions? options = null)
        {
            var programIds = await programRepository.GetIdListByOrganizationIdAsync(organizationId);
            return programIds;
        }

        public async Task<IEnumerable<Course>> GetCoursesByOrganizationIdAsync(long organizationId, ProgramQueryOptions? options = null)
        {
            var courseXProgramService = serviceProvider.GetRequiredService<ICourseXProgramService>();
            var programIds = await programRepository.GetIdListByOrganizationIdAsync(organizationId);
            var coursesList = await courseXProgramService.GetCoursesByProgramId(programIds);
            return coursesList;
        }
    }
}
