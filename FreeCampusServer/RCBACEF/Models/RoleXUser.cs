using System.ComponentModel.DataAnnotations.Schema;

namespace RCBACEF.Models
{
    [Table("RolesXUsers", Schema = "auth")]
    public class RoleXUser : SoftDeletableJoin
    {
        public long RoleId { get; set; }
        public long UserId { get; set; }
        public long CompanyId { get; set; }

        public Role? Role { get; set; }
        public User? User { get; set; }
        public Company? Company { get; set; }
    }
}
