using RFBaseEntities.QueryOptions;

namespace FreeCampusServer.QueryOptions
{
    public class CourseVersionQueryOptions : CommonEntityQueryOptions
    {
        public bool IncludeCourse { get; init; }
    }
}
