using RFBaseEntities.Entities;
using RFRGOBACEntities.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreeCampusServer.Entities
{
    [Table("LearningItemTypes", Schema = "fc")]
    public sealed class LearningItemType : LocalizableEntity
    {
        public long OrganizationId { get; set; }
        public Organization? Organization { get; set; }

        public string? Description { get; set; }

        public LearningItemType() { }

        public LearningItemType(LearningItemType? entity = null)
            : base(entity)
        {
            if (entity == null)
                return;

            OrganizationId = entity.OrganizationId;
            Organization = entity.Organization;

            Description = entity.Description;
        }

        public override LearningItemType Clone()
            => new(this);
    }
}
