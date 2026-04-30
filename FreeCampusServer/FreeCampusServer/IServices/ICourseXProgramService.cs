using FreeCampusServer.Entities;
using FreeCampusServer.QueryOptions;
using RFBaseIServices.IServices;

namespace FreeCampusServer.IServices
{
    public interface ICourseXProgramService : ICommonJoinService<CourseXProgram>
    {
        Task<IEnumerable<Course>> GetCoursesByProgramId(IEnumerable<long> programIds, CourseXProgramQueryOptions? options = null);
    }
}
