using RFBaseEntities.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreeCampusServer.Entities
{
    [Table("CoursePrerequisites", Schema = "fc")]
    public sealed class CoursePrerequisite : CommonJoin
    {
        public long CourseId { get; set; }
        public Course? Course { get; set; }

        public long PrerequisiteId { get; set; }
        public Course? Prerequisite { get; set; }

        public CoursePrerequisite() { }

        public CoursePrerequisite(CoursePrerequisite? entity = null)
            : base(entity)
        {
            if (entity == null)
                return;

            CourseId = entity.CourseId;
            Course = entity.Course;

            PrerequisiteId = entity.PrerequisiteId;
            Prerequisite = entity.Prerequisite;
        }

        public override CoursePrerequisite Clone()
            => new(this);
    }
}
