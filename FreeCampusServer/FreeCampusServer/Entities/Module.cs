using RFBaseEntities.Entities;
using RFRGOBACEntities.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreeCampusServer.Entities
{
    [Table("Modules", Schema = "fc")]
    public class Module : CommonEntity
    {
        public long OrganizationId { get; set; }
        public Organization? Organization { get; set; }

        public long TypeId { get; set; }
        public ModuleType? Type { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

    }
}
