using RFBaseEntities.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreeCampusServer.Entities
{
    [Table("TeachingPermissions ", Schema = "fc")]
    public class TeachingPermission : LocalizableEntity
    {
        public string? Description { get; set; }
    }
}
