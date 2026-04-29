using RFBaseEntities.QueryOptions;

namespace FreeCampusServer.QueryOptions
{
    public class ProgramQueryOptions : CommonEntityQueryOptions
    {
        public bool IncludeOrganization { get; set; } = false;
        public bool IncludeType { get; set; } = true;
    }
}
