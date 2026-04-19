namespace RCBACEF.QueryOptions
{
    public class RoleXUserQueryOptions : ImmutableQueryOptions
    {
        public bool IncludeRole { get; set; } = false;
        public bool IncludeUser { get; set; } = false;
        public bool IncludeCompany { get; set; } = false;
    }
}
