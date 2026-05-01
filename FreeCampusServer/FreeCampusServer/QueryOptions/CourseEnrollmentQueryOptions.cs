using RFBaseEntities.QueryOptions;

namespace FreeCampusServer.QueryOptions
{
    public class CourseEnrollmentQueryOptions : CommonJoinQueryOptions
    {
        public bool IncludeCourse { get; init; }
        public bool IncludeStudent { get; init; }
    }
}
