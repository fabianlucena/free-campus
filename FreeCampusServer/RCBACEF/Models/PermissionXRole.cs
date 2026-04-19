using System.ComponentModel.DataAnnotations.Schema;

namespace RCBACEF.Models
{
    [Table("PermissionsXRoles", Schema = "auth")]
    public class PermissionXRole : Immutable
    {
        public Int64 PermissionId { get; set; }

        public Int64 RoleId { get; set; }


        public Role? Permission { get; set; }

        public Role? Role { get; set; }
    }
}
