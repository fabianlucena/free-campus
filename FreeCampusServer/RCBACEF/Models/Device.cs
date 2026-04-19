using System.ComponentModel.DataAnnotations.Schema;

namespace RCBACEF.Models
{
    [Table("Devices", Schema = "auth")]
    public class Device : Base
    {
        public string Token { get; set; } = string.Empty;
    }
}
