namespace RCBACEF.Models
{
    public class Company : SoftDeletableEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
