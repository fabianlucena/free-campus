namespace RCBACEF.QueryOptions
{
    public class ImmutableQueryOptions : BaseQueryOptions
    {
        public bool IncludeDeleted { get; set; } = false;
        public bool IncludeDeletedBy { get; set; } = false;
    }
}
