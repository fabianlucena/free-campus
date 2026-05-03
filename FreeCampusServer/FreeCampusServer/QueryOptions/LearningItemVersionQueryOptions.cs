using RFBaseEntities.QueryOptions;

namespace FreeCampusServer.QueryOptions
{
    public class LearningItemVersionQueryOptions : CommonEntityQueryOptions
    {
        public bool IncludeLearningItem { get; init; }
    }
}
