namespace RCBACEF.QueryOptions
{
    public class AuditableEntityQueryOptions : CreatableEntityQueryOptions
    {
        public bool IncludeUpdatedBy { get; set; }
    }
}
