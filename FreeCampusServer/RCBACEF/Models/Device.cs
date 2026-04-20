using System.ComponentModel.DataAnnotations.Schema;

namespace RCBACEF.Models
{
    [Table("Devices", Schema = "auth")]
    public class Device : CreatableEntity
    {
        public string Token { get; set; } = string.Empty;
    }
}
