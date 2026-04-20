namespace RCBACEF.Models
{
    public class Permission : ImmutableEntity
    {
        public string Name { get; set; } = string.Empty;

        public string DisplayName { get; set; } = string.Empty;
    }
}
