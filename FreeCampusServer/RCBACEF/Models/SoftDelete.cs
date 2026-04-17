using System.ComponentModel.DataAnnotations.Schema;

namespace RCBACEF.Models
{
    public class SoftDelete : Base
    {
        public DateTime? DeletedAt { get; set; } = null;

        public Int64? DeletedById { get; set; } = 0;

        public User? DeletedBy { get; set; } = null;
    }
}
