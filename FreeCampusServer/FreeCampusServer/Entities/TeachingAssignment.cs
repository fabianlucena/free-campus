using RFBaseEntities.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreeCampusServer.Entities
{
    [Table("TeachingAssignments", Schema = "fc")]
    public sealed class TeachingAssignment : CommonEntity
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

        public TeachingAssignment() { }

        public TeachingAssignment(TeachingAssignment? entity = null)
            : base(entity)
        {
            if (entity == null)
                return;

            CourseId = entity.CourseId;
            Course = entity.Course;

            InstructorId = entity.InstructorId;
            Instructor = entity.Instructor;

            TeachingRoleId = entity.TeachingRoleId;
            TeachingRole = entity.TeachingRole;

            AssignedAt = entity.AssignedAt;
            AssignedById = entity.AssignedById;
            IsActive = entity.IsActive;
            Notes = entity.Notes;
        }

        public override TeachingAssignment Clone()
            => new(this);
    }
}
