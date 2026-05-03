using RFBaseEntities.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreeCampusServer.Entities
{
    [Table("TeachingAssignmentXPermissions", Schema = "fc")]
    public class TeachingAssignmentXPermission : CommonJoin
    {
        public long TeachingAssignmentId { get; set; }
        public TeachingAssignment? TeachingAssignment { get; set; }

        public long PermissionsId { get; set; }
        public TeachingPermission? Permissions { get; set; }
    }
}
