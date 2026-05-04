using RFBaseEntities.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreeCampusServer.Entities
{
    [Table("TeachingRoles", Schema = "fc")]
    public sealed class TeachingRole : LocalizableEntity
    {
        public string? Description { get; set; }

        public TeachingRole() { }

        public TeachingRole(TeachingRole? entity = null)
            : base(entity)
        {
            if (entity == null)
                return;

            Description = entity.Description;
        }

        public override TeachingRole Clone()
            => new(this);
    }
}
