using FreeCampusServer.Entities;

namespace FreeCampusServer.DTO
{
    public class ProgramTypeMinDTO(ProgramType type)
    {
        public Guid Uuid { get; set; } = type.Uuid;
        public string Name { get; } = type.Name;
        public string Title { get; set; } = type.Title;
    }
}
