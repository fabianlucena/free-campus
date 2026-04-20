namespace RCBACEF.Models
{
    public class Entity : Base
    {
        public Int64 Id { get; set; } = 0;
        public Guid Uuid { get; set; } = Guid.Empty;
    }
}
