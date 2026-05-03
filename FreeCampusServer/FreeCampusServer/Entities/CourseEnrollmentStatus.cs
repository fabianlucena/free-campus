using RFBaseEntities.Entities;
using RFRGOBACEntities.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreeCampusServer.Entities
{
    [Table("CourseEnrollmentStatuses", Schema = "fc")]
    public class CourseEnrollmentStatus : TranslatableEntity
    {
        public long OrganizationId { get; set; }
        public Organization? Organization { get; set; }

        public int Order { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
