using System.ComponentModel.DataAnnotations.Schema;

namespace RCBACEF.Models
{
    [Table("RolesXUsers", Schema = "auth")]
    public class RoleXUser : SoftDeletableJoin
    {
        public Int64 RoleId { get; set; }

        public Int64 UserId { get; set; }

        public Int64 CompanyId { get; set; }


        public Role? Role { get; set; }

        public User? User { get; set; }

        public Company? Company { get; set; }
    }
}
