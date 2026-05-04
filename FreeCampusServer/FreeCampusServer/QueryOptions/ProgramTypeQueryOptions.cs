using RFBaseEntities.QueryOptions;

namespace FreeCampusServer.QueryOptions
{
    public class ProgramTypeQueryOptions : CommonEntityQueryOptions
    {
        public bool IncludeOrganization { get; init; }

        public ProgramTypeQueryOptions() { }

        public ProgramTypeQueryOptions(ProgramTypeQueryOptions? options)
            : base(options)
        {
            if (options == null)
                return;

            IncludeOrganization = options.IncludeOrganization;
        }

        public override ProgramTypeQueryOptions Clone()
            => new(this);
    }
}
