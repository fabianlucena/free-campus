namespace RCBACEF.QueryOptions
{
    public class AuditableEntityQueryOptions : BaseQueryOptions
    {
        public bool IncludeUpdatedBy { get; set; } = false;
    }
}
