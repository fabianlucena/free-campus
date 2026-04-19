namespace RCBACEF.QueryOptions
{
    public class SessionQueryOptions : BaseQueryOptions
    {
        public bool IncludeUser { get; set; } = false;
        public bool IncludeDevice { get; set; } = false;
    }
}
