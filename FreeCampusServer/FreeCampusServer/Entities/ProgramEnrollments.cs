using RFBaseEntities.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreeCampusServer.Entities
{
    [Table("ProgramEnrollments", Schema = "fc")]
    public sealed class ProgramEnrollment : CommonEntity
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
        public ProgramEnrollmentStatus? Status { get; set; }

        public Decimal? FinalGrade { get; set; }
        public bool IsActive { get; set; }

        public ProgramEnrollment() { }

        public ProgramEnrollment(ProgramEnrollment? entity = null)
            : base(entity)
        {
            if (entity == null)
                return;

            ProgramVersionId = entity.ProgramVersionId;
            ProgramVersion = entity.ProgramVersion;

            StudentId = entity.StudentId;
            Student = entity.Student;

            EnrolledAt = entity.EnrolledAt;
            EnrolledById = entity.EnrolledById;
            EnrolledBy = entity.EnrolledBy;
            CompletedAt = entity.CompletedAt;
            DroppedAt = entity.DroppedAt;

            StatusId = entity.StatusId;
            Status = entity.Status;

            FinalGrade = entity.FinalGrade;
            IsActive = entity.IsActive;
        }

        public override ProgramEnrollment Clone()
            => new(this);
    }
}
