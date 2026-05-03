using RFBaseEntities.QueryOptions;

namespace FreeCampusServer.QueryOptions
{
    public class LearningItemTypeQueryOptions : CommonEntityQueryOptions
    {
        public bool IncludeOrganization { get; init; }
    }
}
