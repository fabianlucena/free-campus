using System.ComponentModel.DataAnnotations.Schema;

namespace RCBACEF.Models
{
    public class Base
    {
        public Int64 Id { get; set; }

        public Guid Uuid { get; set; } = Guid.NewGuid();

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? DeletedAt { get; set; }
    
        public Int64 CreatedById { get; set; }

        public Int64 UpdatedById { get; set; }

        public Int64? DeletedById { get; set; }
    }
}
