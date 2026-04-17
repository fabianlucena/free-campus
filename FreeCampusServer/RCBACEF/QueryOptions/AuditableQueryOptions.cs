namespace RCBACEF.QueryOptions
{
    public class AuditableQueryOptions : BaseQueryOptions
    {
        public bool IncludeUpdatedBy { get; set; } = false;
    }
}
