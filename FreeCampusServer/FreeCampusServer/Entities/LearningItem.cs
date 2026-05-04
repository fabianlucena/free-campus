using RFBaseEntities.Entities;
using RFRGOBACEntities.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreeCampusServer.Entities
{
    [Table("LearningItems", Schema = "fc")]
    public sealed class LearningItem : CommonEntity
    {
        public long OrganizationId { get; set; }
        public Organization? Organization { get; set; }

        public long TypeId { get; set; }
        public LearningItemType? Type { get; set; } 

        public bool IsActive { get; set; }

        public LearningItem() { }

        public LearningItem(LearningItem? entity = null)
            : base(entity)
        {
            if (entity == null)
                return;

            OrganizationId = entity.OrganizationId;
            Organization = entity.Organization;

            TypeId = entity.TypeId;
            Type = entity.Type;

            IsActive = entity.IsActive;
        }

        public override LearningItem Clone()
            => new(this);
    }
}
