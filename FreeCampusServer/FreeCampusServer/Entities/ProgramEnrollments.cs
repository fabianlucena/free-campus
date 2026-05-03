using RFBaseEntities.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreeCampusServer.Entities
{
    [Table("ProgramEnrollments", Schema = "fc")]
    public class ProgramEnrollment : CommonEntity
    {
        public long ProgramVersionId { get; set; }
        public ProgramVersion? ProgramVersion { get; set; }

        public long StudentId { get; set; }
        public User? Student { get; set; }
    }
}
