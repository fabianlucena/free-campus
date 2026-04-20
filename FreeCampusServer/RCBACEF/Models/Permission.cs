using System.ComponentModel.DataAnnotations.Schema;

namespace RCBACEF.Models
{
    [Table("Permissions", Schema = "auth")]
    public class Permission : ImmutableEntity
    {
        public string Name { get; set; } = string.Empty;
    }
}
