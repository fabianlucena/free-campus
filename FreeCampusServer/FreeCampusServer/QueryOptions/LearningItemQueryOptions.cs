using RFBaseEntities.QueryOptions;

namespace FreeCampusServer.QueryOptions
{
    public sealed class LearningItemQueryOptions : CommonEntityQueryOptions
    {
        public bool IncludeOrganization { get; init; }
        public bool IncludeType { get; init; } = true;

        public LearningItemQueryOptions() { }

        public LearningItemQueryOptions(LearningItemQueryOptions? options)
            : base(options)
        {
            if (options == null)
                return;

            IncludeOrganization = options.IncludeOrganization;
            IncludeType = options.IncludeType;
        }

        public override LearningItemQueryOptions Clone()
            => new(this);
    }
}
