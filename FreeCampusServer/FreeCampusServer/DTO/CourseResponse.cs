using FreeCampusServer.Entities;

namespace FreeCampusServer.DTO
{
    public class CourseResponse(Course course)
    {
        public Guid Uuid { get; set; } = course.Uuid;
        public string Title { get; set; } = course.Title;
        public string Description { get; set; } = course.Description;
        public CourseTypeMinDTO? Type { get; set; } = course.Type != null ? new CourseTypeMinDTO(course.Type) : null;
        public ProgramMinDTO? Program { get; set; } = course.Program != null ? new ProgramMinDTO(course.Program) : null;
    }
}
