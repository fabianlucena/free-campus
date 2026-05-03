using FreeCampusServer.Entities;
using RFRGOBACIServices.DTO;

namespace FreeCampusServer.DTO
{
    public class ModuleResponse(LearningItem learningItem)
    {
        public Guid Uuid { get; set; } = learningItem.Uuid;
        public OrganizationMinDTO? Organization { get; set; } = learningItem.Organization != null ? new OrganizationMinDTO(learningItem.Organization) : null;
        public LearningItemTypeMinDTO? Type { get; set; } = learningItem.Type != null ? new LearningItemTypeMinDTO(learningItem.Type) : null;
        public bool IsActive { get; set; } = learningItem.IsActive;
    }
}
