using RFBaseEntities.QueryOptions;

namespace FreeCampusServer.QueryOptions
{
    public sealed class ProgramQueryOptions : CommonEntityQueryOptions
    {
        public bool IncludeOrganization { get; init; }
        public bool IncludeType { get; init; } = true;

        public long? OrganizationId { get; init; }

        public ProgramQueryOptions() { }

        public ProgramQueryOptions(ProgramQueryOptions? options)
            : base(options)
        {
            if (options == null)
                return;

            IncludeOrganization = options.IncludeOrganization;
            IncludeType = options.IncludeType;

            OrganizationId = options.OrganizationId;
        }

        public override ProgramQueryOptions Clone()
            => new(this);
    }
}
