using System.ComponentModel.DataAnnotations.Schema;

namespace RCBACEF.Models
{
    [Table("RolesIncludes", Schema = "auth")]
    public class RoleInclude : Immutable
    {
        public Int64 RoleId { get; set; }

        public Int64 IncludeId { get; set; }


        public Role? Role { get; set; }

        public Role? Include { get; set; }
    }
}
