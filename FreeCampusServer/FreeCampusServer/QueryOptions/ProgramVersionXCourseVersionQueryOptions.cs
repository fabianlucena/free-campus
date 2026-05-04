using RFBaseEntities.QueryOptions;

namespace FreeCampusServer.QueryOptions
{
    public sealed class ProgramVersionXCourseVersionQueryOptions : CommonEntityQueryOptions
    {
        public bool IncludeProgram { get; init; }
        public bool IncludeCourse { get; init; }

        public ProgramVersionXCourseVersionQueryOptions() { }

        public ProgramVersionXCourseVersionQueryOptions(ProgramVersionXCourseVersionQueryOptions? options)
            : base(options)
        {
            if (options == null)
                return;

            IncludeProgram = options.IncludeProgram;
            IncludeCourse = options.IncludeCourse;
        }

        public override ProgramVersionXCourseVersionQueryOptions Clone()
            => new(this);
    }
}
