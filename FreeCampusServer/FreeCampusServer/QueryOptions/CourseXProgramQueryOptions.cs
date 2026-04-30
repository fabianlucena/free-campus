using RFBaseEntities.QueryOptions;

namespace FreeCampusServer.QueryOptions
{
    public class CourseXProgramQueryOptions : CommonJoinQueryOptions
    {
        public bool IncludeCourse { get; set; } = false;
        public bool IncludeProgram { get; set; } = true;
    }
}
