namespace RCBACEF.QueryOptions
{
    public class SoftDeletableJoinQueryOptions : BaseQueryOptions
    {
        public bool IncludeDeleted { get; set; } = false;
        public bool IncludeDeletedBy { get; set; } = false;
    }
}
