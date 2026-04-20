namespace RCBACEF.QueryOptions
{
    public class SoftDeletableEntityQueryOptions : AuditableEntityQueryOptions
    {
        public bool IncludeDeleted { get; set; } = false;
        public bool IncludeDeletedBy { get; set; } = false;
    }
}
