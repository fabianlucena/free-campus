using FreeCampusServer.Entities;
using FreeCampusServer.IRepository;
using FreeCampusServer.IServices;
using RFBaseServices.Services;

namespace FreeCampusServer.Service
{
    public class CourseEnrollmentService(ICourseEnrollmentRepository courseEnrollmentRepository)
        : CommonEntityService<CourseEnrollment>(courseEnrollmentRepository),
        ICourseEnrollmentService
    {
    }
}
