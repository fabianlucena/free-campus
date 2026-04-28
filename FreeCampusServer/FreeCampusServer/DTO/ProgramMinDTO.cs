namespace FreeCampusServer.DTO
{
    public class ProgramMinDTO(Entities.Program program)
    {
        public Guid Uuid { get; set; } = program.Uuid;
        public string Title { get; set; } = program.Title;
        public string Description { get; set; } = program.Description;
    }
}
