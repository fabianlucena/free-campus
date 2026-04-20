namespace RCBACEF.QueryOptions
{
    public class PermissionXRoleQueryOptions : SoftDeletableJoinQueryOptions
    {
        public bool IncludePermission { get; set; } = false;
        public bool IncludeRole { get; set; } = false;
    }
}
