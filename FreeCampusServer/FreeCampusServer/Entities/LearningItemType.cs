using RFBaseEntities.Entities;
using RFRGOBACEntities.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreeCampusServer.Entities
{
    [Table("LearningItemTypes", Schema = "fc")]
    public class LearningItemType : LocalizableEntity
    {
        public long OrganizationId { get; set; }
        public Organization? Organization { get; set; }

        public string? Description { get; set; }
    }
}
