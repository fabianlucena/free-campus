using RFBaseEntities.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreeCampusServer.Entities
{
    [Table("TeachingAssignments", Schema = "fc")]
    public class TeachingAssignment : CommonEntity
    {
        public long CourseId { get; set; }
        public Course? Course { get; set; }

        public long InstructorId { get; set; }
        public User? Instructor { get; set; }

        public long TeachingRoleId { get; set; }
        public TeachingRole? TeachingRole { get; set; }

        public DateTime AssignedAt { get; set; }
        public User? AssignedById { get; set; }
        public bool IsActive { get; set; }
        public string? Notes { get; set; }
    }
}
