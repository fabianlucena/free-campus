using RFBaseEntities.QueryOptions;

namespace FreeCampusServer.QueryOptions
{
    public sealed class LearningItemTypeQueryOptions : CommonEntityQueryOptions
    {
        public bool IncludeOrganization { get; init; }

        public LearningItemTypeQueryOptions() { }

        public LearningItemTypeQueryOptions(LearningItemTypeQueryOptions? options)
            : base(options)
        {
            if (options == null)
                return;

            IncludeOrganization = options.IncludeOrganization;
        }

        public override LearningItemTypeQueryOptions Clone()
            => new(this);
    }
}
