using RFBaseEntities.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreeCampusServer.Entities
{
    [Table("CourseVersionXLearningItemVersions", Schema = "fc")]
    public class CourseVersionXLearningItemVersion : CommonEntity
    {
        public long CourseVersionId { get; set; }
        public CourseVersion? CourseVersion { get; set; }

        public long LearningItemVersionId { get; set; }
        public LearningItemVersion? LearningItemVersion { get; set; }

        public long ParentLearningItemVersionId { get; set; }
        public LearningItemVersion? ParentLearningItemVersion { get; set; }

        public int Order { get; set; }
        public bool IsActive { get; set; }
        public bool IsPublished { get; set; }
    }
}
