using RFBaseEntities.Entities;
using RFRGOBACEntities.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreeCampusServer.Entities
{
    [Table("CourseTypes", Schema = "fc")]
    public sealed class CourseType : LocalizableEntity
    {
        public long OrganizationId { get; set; }
        public Organization? Organization { get; set; }

        public string? Description { get; set; }

        public CourseType() { }

        public CourseType(CourseType entity)
            : base(entity)
        {
            OrganizationId = entity.OrganizationId;
            Organization = entity.Organization;
            Description = entity.Description;
        }

        public override CourseType Clone()
            => new(this);
    }
}
