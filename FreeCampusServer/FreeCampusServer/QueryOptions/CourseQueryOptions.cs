using RFBaseEntities.QueryOptions;

namespace FreeCampusServer.QueryOptions
{
    public class CourseQueryOptions : CommonEntityQueryOptions
    {
        public bool IncludeOrganization { get; set; } = false;
        public bool IncludeType { get; set; } = true;
    }
}
