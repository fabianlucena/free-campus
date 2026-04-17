namespace RCBACEF.QueryOptions
{
    public class SoftDeletableQueryOptions : AuditableQueryOptions
    {
        public bool IncludeDeleted { get; set; } = false;
        public bool IncludeDeletedBy { get; set; } = false;
    }
}
