using RFRGOBACIServices.DTO;

namespace FreeCampusServer.DTO
{
    public class ProgramMinDTO(Entities.Program program)
    {
        public Guid Uuid { get; set; } = program.Uuid;
        public OrganizationMinDTO? Organization { get; set; } = program.Organization != null ? new OrganizationMinDTO(program.Organization) : null;
        public ProgramTypeMinDTO? Type { get; set; } = program.Type != null ? new ProgramTypeMinDTO(program.Type) : null;
        public string Title { get; set; } = program.Title;
        public string Description { get; set; } = program.Description;
    }
}
