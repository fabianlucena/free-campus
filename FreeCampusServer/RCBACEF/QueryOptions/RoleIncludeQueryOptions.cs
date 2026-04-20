using RCBACEF.Models;

namespace RCBACEF.QueryOptions
{
    public class RoleIncludeQueryOptions : SoftDeletableJoinQueryOptions
    {
        public bool IncludeRole { get; set; } = false;
        public bool IncludeInclude { get; set; } = false;
    }
}
