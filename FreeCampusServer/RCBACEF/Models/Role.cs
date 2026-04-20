namespace RCBACEF.Models
{
    public class Role : SoftDeletableEntity
    {
        public string Name { get; set; } = string.Empty;

        public string DisplayName { get; set; } = string.Empty;
    }
}
