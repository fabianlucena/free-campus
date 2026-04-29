using RFBaseEntities.QueryOptions;

namespace FreeCampusServer.QueryOptions
{
    public class ModuleTypeQueryOptions : CommonEntityQueryOptions
    {
        public bool IncludeOrganization { get; set; } = false;
    }
}
