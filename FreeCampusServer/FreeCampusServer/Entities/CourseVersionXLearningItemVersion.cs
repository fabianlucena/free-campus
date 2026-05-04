using RFBaseEntities.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreeCampusServer.Entities
{
    [Table("CourseVersionXLearningItemVersions", Schema = "fc")]
    public sealed class CourseVersionXLearningItemVersion : CommonEntity
    {
        public long CourseVersionId { get; set; }
        public CourseVersion? CourseVersion { get; set; }

        public long LearningItemVersionId { get; set; }
        public LearningItemVersion? LearningItemVersion { get; set; }

        public long ParentLearningItemVersionId { get; set; }
        public LearningItemVersion? ParentLearningItemVersion { get; set; }

        public int SequenceNumber { get; set; }
        public bool IsActive { get; set; }
        public bool IsPublished { get; set; }

        public CourseVersionXLearningItemVersion() { }

        public CourseVersionXLearningItemVersion(CourseVersionXLearningItemVersion? entity = null)
            : base(entity)
        {
            if (entity == null)
                return;

            CourseVersionId = entity.CourseVersionId;
            CourseVersion = entity.CourseVersion;

            LearningItemVersionId = entity.LearningItemVersionId;
            LearningItemVersion = entity.LearningItemVersion;

            ParentLearningItemVersionId = entity.ParentLearningItemVersionId;
            ParentLearningItemVersion = entity.ParentLearningItemVersion;

            SequenceNumber = entity.SequenceNumber;
            IsActive = entity.IsActive;
            IsPublished = entity.IsPublished;
        }

        public override CourseVersionXLearningItemVersion Clone()
            => new(this);
    }
}
