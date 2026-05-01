using RFBaseEntities.QueryOptions;

namespace FreeCampusServer.QueryOptions
{
    public class CourseTypeQueryOptions : CommonEntityQueryOptions
    {
        public bool IncludeOrganization { get; init; }
    }
}
