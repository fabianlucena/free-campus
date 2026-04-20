namespace RCBACEF.Models
{
    public class SoftDeletableJoin : CreatableJoin
    {
        public DateTime? DeletedAt { get; set; } = null;

        public Int64? DeletedById { get; set; } = 0;

        public User? DeletedBy { get; set; } = null;
    }
}