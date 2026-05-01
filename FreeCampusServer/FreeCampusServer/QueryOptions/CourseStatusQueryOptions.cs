using RFBaseEntities.QueryOptions;

namespace FreeCampusServer.QueryOptions
{
    public class CourseStatusQueryOptions : NominableEntityQueryOptions
    {
        public bool IncludeOrganization { get; init; }
    }
}
