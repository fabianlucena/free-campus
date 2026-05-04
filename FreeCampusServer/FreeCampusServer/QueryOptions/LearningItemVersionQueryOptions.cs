using RFBaseEntities.QueryOptions;

namespace FreeCampusServer.QueryOptions
{
    public class LearningItemVersionQueryOptions : CommonEntityQueryOptions
    {
        public bool IncludeLearningItem { get; init; }

        public LearningItemVersionQueryOptions() { }

        public LearningItemVersionQueryOptions(LearningItemVersionQueryOptions? options)
            : base(options)
        {
            if (options == null)
                return;

            IncludeLearningItem = options.IncludeLearningItem;
        }

        public override LearningItemVersionQueryOptions Clone()
            => new(this);
    }
}
