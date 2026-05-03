using RFBaseEntities.QueryOptions;

namespace FreeCampusServer.QueryOptions
{
    public class ProgramVersionQueryOptions : CommonEntityQueryOptions
    {
        public bool IncludeProgram { get; init; }
    }
}
