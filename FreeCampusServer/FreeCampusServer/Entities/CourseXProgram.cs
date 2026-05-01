using RFBaseEntities.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreeCampusServer.Entities
{
    [Table("CoursesXPrograms", Schema = "fc")]
    public class CourseXProgram : CommonEntity
    {
        public long CourseId { get; set; }
        public Course? Course { get; set; }

        public long ProgramId { get; set; }
        public Program? Program { get; set; }

        public string Code { get; set; } = string.Empty;

        public int Order { get; set; }
        public int Level { get; set; }

        public bool IsCore { get; set; }
        public bool IsElective { get; set; }
    }
}
