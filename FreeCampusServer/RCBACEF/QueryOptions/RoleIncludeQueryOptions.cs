using RCBACEF.Models;

namespace RCBACEF.QueryOptions
{
    public class RoleIncludeQueryOptions : ImmutableQueryOptions
    {
        public bool IncludeRole { get; set; } = false;
        public bool IncludeInclude { get; set; } = false;
    }
}
