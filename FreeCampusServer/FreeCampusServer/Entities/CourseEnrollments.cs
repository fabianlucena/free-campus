using RFBaseEntities.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreeCampusServer.Entities
{
    [Table("CourseEnrollments", Schema = "fc")]
    public class CourseEnrollment : CommonEntity
    {
        public long CourseId { get; set; }
        public Course? Course { get; set; }

        public long StudentId { get; set; }
        public User? Student { get; set; }

        public DateTime EnrolledAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public DateTime? DroppedAt { get; set; }

        public long StatusId { get; set; }
        public CourseStatus? Status { get; set; }

        public Decimal? FinalGrade { get; set; }
        public double Progress { get; set; }
        public bool IsActive { get; set; }
    }
}
