using FreeCampusServer.Entities;
using FreeCampusServer.QueryOptions;
using RFBaseIServices.IServices;

namespace FreeCampusServer.IServices
{
    public interface ICourseXProgramService : ICommonEntityService<CourseXProgram>
    {
        Task<IEnumerable<Course>> GetCoursesAsync(CourseXProgramQueryOptions options);
    }
}
