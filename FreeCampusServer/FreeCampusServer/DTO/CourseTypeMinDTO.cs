using FreeCampusServer.Entities;

namespace FreeCampusServer.DTO
{
    public class CourseTypeMinDTO(CourseType type)
    {
        public Guid Uuid { get; set; } = type.Uuid;
        public string Title { get; set; } = type.Title;
        public string Description { get; set; } = type.Description;
    }
}
