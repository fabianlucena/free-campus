using RFBaseEntities.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreeCampusServer.Entities
{
    [Table("ProgramEnrollments", Schema = "fc")]
    public class ProgramEnrollment : CommonEntity
    {
        public long ProgramVersionId { get; set; }
        public ProgramVersion? ProgramVersion { get; set; }

        public long StudentId { get; set; }
        public User? Student { get; set; }

        public DateTime EnrolledAt { get; set; }
        public long EnrolledById { get; set; }
        public User? EnrolledBy { get; set; }
        public DateTime? CompletedAt { get; set; }
        public DateTime? DroppedAt { get; set; }

        public long StatusId { get; set; }
        public CourseStatus? Status { get; set; }

        public Decimal? FinalGrade { get; set; }
        public bool IsActive { get; set; }
    }
}
