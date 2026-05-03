using FreeCampusServer.Entities;
using FreeCampusServer.IRepository;
using FreeCampusServer.IServices;
using RFBaseServices.Services;

namespace FreeCampusServer.Service
{
    public class CourseVersionService(ICourseVersionRepository courseVersionRepository)
        : CommonEntityService<CourseVersion>(courseVersionRepository),
        ICourseVersionService
    {
    }
}
