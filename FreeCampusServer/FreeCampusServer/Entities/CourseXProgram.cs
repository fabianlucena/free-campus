using RFBaseEntities.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreeCampusServer.Entities
{
    [Table("CoursesXPrograms", Schema = "fc")]
    public class CourseXProgram : CommonJoin
    {
        public long CourseId { get; set; }
        public Course? Course { get; set; }

        public long ProgramId { get; set; }
        public Program? Program { get; set; }
    }
}
