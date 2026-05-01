using RFBaseEntities.QueryOptions;

namespace FreeCampusServer.QueryOptions
{
    public class CourseXProgramQueryOptions : CommonJoinQueryOptions
    {
        public bool IncludeCourse { get; init; }
        public bool IncludeProgram { get; init; }


        public CourseXProgramQueryOptions() { }

        public CourseXProgramQueryOptions(CourseXProgramQueryOptions options)
        {
            IncludeCourse = options.IncludeCourse;
            IncludeProgram = options.IncludeProgram;
        }
    }
}
