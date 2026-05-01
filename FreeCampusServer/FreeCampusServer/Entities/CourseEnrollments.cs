using RFBaseEntities.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreeCampusServer.Entities
{
    [Table("CourseEnrollments", Schema = "fc")]
    public class CourseEnrollment : CommonEntity
    {
        public long CourseId { get; set; }
        public Course? Course { get; set; }

        public long StudentId { get; set; }
        public User? Student { get; set; }
    }
}
