using RFBaseEntities.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreeCampusServer.Entities
{
    [Table("Courses", Schema = "fc")]
    public class Course : CommonEntity
    {
        public long TypeId { get; set; }
        public CourseType? Type { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public long? ProgramId { get; set; }
        public Program? Program { get; set; }
    }
}
