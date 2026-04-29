using FreeCampusServer.Entities;
using RFRGOBACIServices.DTO;

namespace FreeCampusServer.DTO
{
    public class ModuleResponse(Module module)
    {
        public Guid Uuid { get; set; } = module.Uuid;
        public OrganizationMinDTO? Organization { get; set; } = module.Organization != null ? new OrganizationMinDTO(module.Organization) : null;
        public ModuleTypeMinDTO? Type { get; set; } = module.Type != null ? new ModuleTypeMinDTO(module.Type) : null;
        public string Title { get; set; } = module.Title;
        public string Description { get; set; } = module.Description;
    }
}
