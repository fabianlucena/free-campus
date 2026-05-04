using RFBaseEntities.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreeCampusServer.Entities
{
    [Table("CourseVersions", Schema = "fc")]
    public sealed class CourseVersion : CommonEntity
    {
        public long CourseId { get; set; }
        public Course? Course { get; set; }

        public int VersionNumber { get; set; }
        public string? VersionLabel { get; set; }
        public long? PreviousVersionId { get; set; }
        public CourseVersion? PreviousVersion { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int? Credits { get; set; }
        public int? Hours { get; set; }

        public CourseVersion() { }

        public CourseVersion(CourseVersion? entity = null)
            : base(entity)
        {
            if (entity == null)
                return;

            CourseId = entity.CourseId;
            Course = entity.Course;

            VersionNumber = entity.VersionNumber;
            VersionLabel = entity.VersionLabel;
            PreviousVersionId = entity.PreviousVersionId;
            PreviousVersion = entity.PreviousVersion;

            Title = entity.Title;
            Description = entity.Description;
            Credits = entity.Credits;
            Hours = entity.Hours;
        }

        public override CourseVersion Clone()
            => new(this);
    }
}
