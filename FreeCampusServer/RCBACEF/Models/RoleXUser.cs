namespace RCBACEF.Models
{
    public class RoleXUser
    {
        public Int64 RoleId { get; set; }

        public Int64 UserId { get; set; }

        public Int64 CompanyId { get; set; }

        public Role? Role { get; set; }

        public User? User { get; set; }

        public Company? Company { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? DeletedAt { get; set; }

        public Int64 CreatedById { get; set; }

        public Int64? DeletedById { get; set; }

        public User? CreatedBy { get; set; }

        public User? DeletedBy { get; set; }
    }
}
