using RFBaseEntities.QueryOptions;

namespace FreeCampusServer.QueryOptions
{
    public class ProgramQueryOptions : CommonEntityQueryOptions
    {
        public bool IncludeType { get; set; } = true;
        public bool IncludeCompany { get; set; } = true;
    }
}
