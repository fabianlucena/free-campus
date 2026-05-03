using RFBaseEntities.QueryOptions;

namespace FreeCampusServer.QueryOptions
{
    public class LearningItemQueryOptions : CommonEntityQueryOptions
    {
        public bool IncludeOrganization { get; init; }
        public bool IncludeType { get; init; } = true;
    }
}
