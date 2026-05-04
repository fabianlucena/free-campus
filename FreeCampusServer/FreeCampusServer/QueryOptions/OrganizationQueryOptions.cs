using RFAuthEntities.QueryOptions;
using RFBaseEntities.QueryOptions;

namespace FreeCampusServer.QueryOptions
{
    public class OrganizationQueryOptions : LocalizableEntityQueryOptions
    {
        public bool IsActive { get; init; }
        public string? Description { get; init; }

        public OrganizationQueryOptions() { }

        public OrganizationQueryOptions(OrganizationQueryOptions? options)
            : base(options)
        {
            if (options == null)
                return;

            IsActive = true;
            Description = options.Description;
        }

        public override OrganizationQueryOptions Clone()
            => new(this);
    }
}