namespace RCBACEF.Models
{
    public class Permission : SoftDeletable
    {
        public string Name { get; set; } = string.Empty;

        public string DisplayName { get; set; } = string.Empty;
    }
}
