using RFBaseEntities.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreeCampusServer.Entities
{
    [Table("ProgramVersionXCourseVersions", Schema = "fc")]
    public sealed class ProgramVersionXCourseVersion : CommonEntity
    {
        public long ProgramVersionId { get; set; }
        public ProgramVersion? ProgramVersion { get; set; }

        public long CourseVersionId { get; set; }
        public CourseVersion? CourseVersion { get; set; }

        public string Code { get; set; } = string.Empty;

        public int SequenceNumber { get; set; }
        public int Level { get; set; }

        public bool IsActive { get; set; }
        public bool IsCore { get; set; }
        public bool IsRequired { get; set; }
        public bool IsElective { get; set; }

        public ProgramVersionXCourseVersion() { }

        public ProgramVersionXCourseVersion(ProgramVersionXCourseVersion? entity = null)
            : base(entity)
        {
            if (entity == null)
                return;

            ProgramVersionId = entity.ProgramVersionId;
            ProgramVersion = entity.ProgramVersion;

            CourseVersionId = entity.CourseVersionId;
            CourseVersion = entity.CourseVersion;
            Code = entity.Code;

            SequenceNumber = entity.SequenceNumber;
            Level = entity.Level;

            IsActive = entity.IsActive;
            IsCore = entity.IsCore;
            IsRequired = entity.IsRequired;
            IsElective = entity.IsElective;
        }

        public override ProgramVersionXCourseVersion Clone()
            => new(this);
    }
}
