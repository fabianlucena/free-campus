using FreeCampusServer.Entities;
using RFRGOBACIServices.DTO;

namespace FreeCampusServer.DTO
{
    public class CourseResponse(Course course)
    {
        public Guid Uuid { get; set; } = course.Uuid;
        public OrganizationMinDTO? Organization { get; set; } = course.Organization != null ? new OrganizationMinDTO(course.Organization) : null;
        public CourseTypeMinDTO? Type { get; set; } = course.Type != null ? new CourseTypeMinDTO(course.Type) : null;
    }
}
