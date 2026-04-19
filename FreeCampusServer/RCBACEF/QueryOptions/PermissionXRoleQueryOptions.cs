namespace RCBACEF.QueryOptions
{
    public class PermissionXRoleQueryOptions : ImmutableQueryOptions
    {
        public bool IncludePermission { get; set; } = false;
        public bool IncludeRole { get; set; } = false;
    }
}
