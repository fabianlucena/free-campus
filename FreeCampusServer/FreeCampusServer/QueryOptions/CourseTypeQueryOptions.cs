using RFBaseEntities.QueryOptions;

namespace FreeCampusServer.QueryOptions
{
    public class CourseTypeQueryOptions : CommonEntityQueryOptions
    {
        public bool IncludeOrganization { get; init; }

        public CourseTypeQueryOptions() { }

        public CourseTypeQueryOptions(CourseTypeQueryOptions? options)
            : base(options)
        {
            if (options == null)
                return;

            IncludeOrganization = options.IncludeOrganization;
        }

        public override CourseTypeQueryOptions Clone()
            => new(this);
    }
}
