using System.ComponentModel.DataAnnotations.Schema;

namespace RCBACEF.Models
{
    [Table("Sessions", Schema = "auth")]
    public class Session : CreatableEntity
    {
        public string Token { get; set; } = string.Empty;
        public DateTime ExpireAt { get; set; } = DateTime.MinValue;
        public string AutoLoginToken { get; set; } = string.Empty;
        public DateTime LastUsedAt { get; set; } = DateTime.MinValue;

        public Int64 UserId { get; set; }
        public User? User { get; set; }

        public Int64 DeviceId { get; set; }
        public Device? Device { get; set; }

        public Int64? CompanyId { get; set; }
        public Company? Company { get; set; }
    }
}
