using RFBaseEntities.Entities;
using RFRGOBACEntities.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreeCampusServer.Entities
{
    [Table("LearningItems", Schema = "fc")]
    public class LearningItem : CommonEntity
    {
        public long OrganizationId { get; set; }
        public Organization? Organization { get; set; }

        public long TypeId { get; set; }
        public LearningItemType? Type { get; set; } 

        public bool IsActive { get; set; }
    }
}
