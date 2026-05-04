using RFBaseEntities.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreeCampusServer.Entities
{
    [Table("TeachingAssignmentXPermissions", Schema = "fc")]
    public sealed class TeachingAssignmentXPermission : CommonJoin
    {
        public long TeachingAssignmentId { get; set; }
        public TeachingAssignment? TeachingAssignment { get; set; }

        public long PermissionsId { get; set; }
        public TeachingPermission? Permissions { get; set; }

        public TeachingAssignmentXPermission() { }

        public TeachingAssignmentXPermission(TeachingAssignmentXPermission? entity = null)
            : base(entity)
        {
            if (entity == null)
                return;

            TeachingAssignmentId = entity.TeachingAssignmentId;
            TeachingAssignment = entity.TeachingAssignment;

            PermissionsId = entity.PermissionsId;
            Permissions = entity.Permissions;
        }

        public override TeachingAssignmentXPermission Clone()
            => new(this);
    }
}
