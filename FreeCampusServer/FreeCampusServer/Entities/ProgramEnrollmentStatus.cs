using RFBaseEntities.Entities;
using RFRGOBACEntities.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreeCampusServer.Entities
{
    [Table("ProgramEnrollmentStatuses", Schema = "fc")]
    public sealed class ProgramEnrollmentStatus : TranslatableEntity
    {
        public long OrganizationId { get; set; }
        public Organization? Organization { get; set; }

        public int DisplayOrder { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public ProgramEnrollmentStatus() { }

        public ProgramEnrollmentStatus(ProgramEnrollmentStatus? entity = null)
            : base(entity)
        {
            if (entity == null)
                return;

            OrganizationId = entity.OrganizationId;
            Organization = entity.Organization;

            DisplayOrder = entity.DisplayOrder;
            Name = entity.Name;
            IsActive = entity.IsActive;
            Title = entity.Title;
            Description = entity.Description;
        }

        public override ProgramEnrollmentStatus Clone()
            => new(this);
    }
}
