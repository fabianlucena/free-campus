using RFBaseEntities.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreeCampusServer.Entities
{
    [Table("CoursePrerequisites", Schema = "fc")]
    public class CoursePrerequisite : CommonJoin
    {
        public long CourseId { get; set; }
        public Course? Course { get; set; }

        public long PrerequisiteId { get; set; }
        public Course? Prerequisite { get; set; }
    }
}
