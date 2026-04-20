namespace RCBACEF.Models
{
    public class Permission : SoftDeletableEntity
    {
        public string Name { get; set; } = string.Empty;

        public string DisplayName { get; set; } = string.Empty;
    }
}
