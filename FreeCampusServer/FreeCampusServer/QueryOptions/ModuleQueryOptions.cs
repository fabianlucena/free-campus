using RFBaseEntities.QueryOptions;

namespace FreeCampusServer.QueryOptions
{
    public class ModuleQueryOptions : CommonEntityQueryOptions
    {
        public bool IncludeType { get; set; } = true;
        public bool IncludeCourse { get; set; } = false;
    }
}
