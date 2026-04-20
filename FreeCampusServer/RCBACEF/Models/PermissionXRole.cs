using System.ComponentModel.DataAnnotations.Schema;

namespace RCBACEF.Models
{
    [Table("PermissionsXRoles", Schema = "auth")]
    public class PermissionXRole : SoftDeletableJoin
    {
        public long PermissionId { get; set; }

        public long RoleId { get; set; }


        public Role? Permission { get; set; }

        public Role? Role { get; set; }
    }
}
