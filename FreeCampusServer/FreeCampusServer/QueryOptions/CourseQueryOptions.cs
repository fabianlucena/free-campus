using RFBaseEntities.QueryOptions;

namespace FreeCampusServer.QueryOptions
{
    public class CourseQueryOptions : CommonEntityQueryOptions
    {
        public bool IncludeType { get; set; } = true;
        public bool IncludeProgram{ get; set; } = false;
    }
}
