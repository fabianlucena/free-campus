using RFBaseEntities.Entities;
using RFRGOBACEntities.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreeCampusServer.Entities
{
    [Table("ProgramTypes", Schema = "fc")]
    public sealed class ProgramType : LocalizableEntity
    {
        public long OrganizationId { get; set; }
        public Organization? Organization { get; set; }

        public string? Description { get; set; }

        public ProgramType() { }

        public ProgramType(ProgramType? entity = null)
            : base(entity)
        {
            if (entity == null)
                return;

            OrganizationId = entity.OrganizationId;
            Organization = entity.Organization;

            Description = entity.Description;
        }

        public override ProgramType Clone()
            => new(this);
    }
}
