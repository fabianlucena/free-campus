using System.ComponentModel.DataAnnotations.Schema;

namespace RCBACEF.Models
{
    [Table("Roles", Schema = "auth")]
    public class Role : SoftDeletableEntity
    {
        public string Name { get; set; } = string.Empty;

        public string DisplayName { get; set; } = string.Empty;
    }
}
