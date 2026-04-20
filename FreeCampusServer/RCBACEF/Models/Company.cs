using System.ComponentModel.DataAnnotations.Schema;

namespace RCBACEF.Models
{
    [Table("Companies", Schema = "auth")]
    public class Company : SoftDeletableEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
