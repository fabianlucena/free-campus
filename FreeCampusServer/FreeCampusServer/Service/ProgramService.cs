using FreeCampusServer.Entities;
using FreeCampusServer.IRepository;
using FreeCampusServer.IServices;
using FreeCampusServer.QueryOptions;
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
        public async Task<IEnumerable<long>> GetIdListAsync(ProgramQueryOptions options)
            => await programRepository.GetIdListAsync(options);

        public async Task<IEnumerable<Course>> GetAvailableCoursesAsync(ProgramQueryOptions options)
        {
            var courseXProgramService = serviceProvider.GetRequiredService<ICourseXProgramService>();
            var programIds = await programRepository.GetIdListAsync(options);
            var coursesList = await courseXProgramService.GetCoursesByProgramId(programIds);
            return coursesList;
        }
    }
}
