using RFBaseEntities.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreeCampusServer.Entities
{
    [Table("TeachingRoles", Schema = "fc")]
    public class TeachingRole : LocalizableEntity
    {
        public string? Description { get; set; }

        public TeachingRole() { }

        public TeachingRole(TeachingRole entity)
            : base(entity)
        {
            Description = entity.Description;
        }

        public override TeachingRole Clone()
        {
            return new TeachingRole(this);
        }
    }
}
