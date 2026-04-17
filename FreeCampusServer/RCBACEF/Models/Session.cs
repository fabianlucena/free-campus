namespace RCBACEF.Models
{
    public class Session : Base
    {
        public string Token { get; set; } = string.Empty;
        public DateTime ExpireAt { get; set; } = DateTime.MinValue;
        public string AutoLoginToken { get; set; } = string.Empty;

        public Int64 UserId { get; set; }
        public User? User { get; set; }

        public Int64 DeviceId { get; set; }
        public Device? Device { get; set; }
    }
}
