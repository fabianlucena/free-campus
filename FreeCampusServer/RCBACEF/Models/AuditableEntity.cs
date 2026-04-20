namespace RCBACEF.Models
{
    public class AuditableEntity : CreatableEntity
    {
        public DateTime UpdatedAt { get; set; } = DateTime.MinValue;
        public Int64 UpdatedById { get; set; } = 0;
        public User? UpdatedBy { get; set; } = null;
    }
}
