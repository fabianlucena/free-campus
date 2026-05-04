using RFBaseEntities.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreeCampusServer.Entities
{
    [Table("TeachingPermissions ", Schema = "fc")]
    public sealed class TeachingPermission : LocalizableEntity
    {
        public string? Description { get; set; }

        public TeachingPermission() { }

        public TeachingPermission(TeachingPermission? entity = null)
            : base(entity)
        {
            if (entity == null)
                return;

            Description = entity.Description;
        }

        public override TeachingPermission Clone()
            => new(this);
    }
}
