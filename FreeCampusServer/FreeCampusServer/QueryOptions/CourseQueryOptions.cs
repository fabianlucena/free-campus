using RFBaseEntities.QueryOptions;

namespace FreeCampusServer.QueryOptions
{
    public class CourseQueryOptions : CommonEntityQueryOptions
    {
        public bool IncludeOrganization { get; init; }
        public bool IncludeType { get; init; } = true;

        public long? OrganizationId { get; init; }
        public long? StudentId { get; init; }
        public bool? Standalone { get; init; }
        public long? ExcludeStudentId { get; init; }
    }
}
