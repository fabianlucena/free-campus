using RFBaseEntities.QueryOptions;

namespace FreeCampusServer.QueryOptions
{
    public sealed class CourseVersionQueryOptions : CommonEntityQueryOptions
    {
        public bool IncludeCourse { get; init; }

        public CourseVersionQueryOptions() { }

        public CourseVersionQueryOptions(CourseVersionQueryOptions? options)
            : base(options)
        {
            if (options == null)
                return;

            IncludeCourse = options.IncludeCourse;
        }

        public override CourseVersionQueryOptions Clone()
            => new(this);
    }
}
