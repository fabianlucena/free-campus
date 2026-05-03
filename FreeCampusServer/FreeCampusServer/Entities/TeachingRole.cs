using RFBaseEntities.Entities;
using RFRGOBACEntities.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreeCampusServer.Entities
{
    [Table("TeachingRoles", Schema = "fc")]
    public class TeachingRole : LocalizableEntity
    {
        public long OrganizationId { get; set; }
        public Organization? Organization { get; set; }

        public string? Description { get; set; }
    }
}
