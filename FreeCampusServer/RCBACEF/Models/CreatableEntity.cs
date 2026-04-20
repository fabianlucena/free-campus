namespace RCBACEF.Models
{
    public class CreatableEntity : Entity
    {
        public DateTime CreatedAt { get; set; } = DateTime.MinValue;
        public Int64 CreatedById { get; set; } = 0;
        public User? CreatedBy { get; set; } = null;
    }
}
