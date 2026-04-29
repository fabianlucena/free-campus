using RFBaseEntities.QueryOptions;

namespace FreeCampusServer.QueryOptions
{
    public class ProgramTypeQueryOptions : CommonEntityQueryOptions
    {
        public bool IncludeOrganization { get; set; } = false;
    }
}
