using FreeCampusServer.Entities;
using FreeCampusServer.IRepository;
using FreeCampusServer.IServices;
using RFBaseServices.Services;

namespace FreeCampusServer.Service
{
    public class CourseXProgramService(ICourseXProgramRepository courseXProgramRepository)
        : CommonEntityService<CourseXProgram>(courseXProgramRepository),
        ICourseXProgramService
    {
    }
}
