using RFBaseEntities.QueryOptions;

namespace FreeCampusServer.QueryOptions
{
    public class ProgramVersionXCourseVersionQueryOptions : CommonEntityQueryOptions
    {
        public bool IncludeProgram { get; init; }
        public bool IncludeCourse { get; init; }

        public ProgramVersionXCourseVersionQueryOptions() { }

        public ProgramVersionXCourseVersionQueryOptions(ProgramVersionXCourseVersionQueryOptions options)
        {
            IncludeProgram = options.IncludeProgram;
            IncludeCourse = options.IncludeCourse;
        }
    }
}
