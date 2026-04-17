using System.ComponentModel.DataAnnotations.Schema;

namespace RCBACEF.Models
{
    public class Base
    {
        public Int64 Id { get; set; } = 0;
        public Guid Uuid { get; set; } = Guid.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.MinValue;
        public DateTime UpdatedAt { get; set; } = DateTime.MinValue;

        public Int64 CreatedById { get; set; } = 0;
        public Int64 UpdatedById { get; set; } = 0;

        public User? CreatedBy { get; set; } = null;
        public User? UpdatedBy { get; set; } = null;
    }
}
