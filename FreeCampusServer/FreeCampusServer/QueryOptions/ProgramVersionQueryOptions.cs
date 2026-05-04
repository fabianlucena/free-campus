using RFBaseEntities.QueryOptions;

namespace FreeCampusServer.QueryOptions
{
    public sealed class ProgramVersionQueryOptions : CommonEntityQueryOptions
    {
        public bool IncludeProgram { get; init; }

        public ProgramVersionQueryOptions() { }

        public ProgramVersionQueryOptions(ProgramVersionQueryOptions? options)
            : base(options)
        {
            if (options == null)
                return;

            IncludeProgram = options.IncludeProgram;
        }

        public override ProgramVersionQueryOptions Clone()
            => new(this);
    }
}
