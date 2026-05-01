using RFBaseEntities.QueryOptions;

namespace FreeCampusServer.QueryOptions
{
    public class CourseQueryOptions : CommonEntityQueryOptions
    {
        public bool IncludeOrganization { get; init; }
        public bool IncludeType { get; init; } = true;

        public long? OrganizationId { get; init; }
        public long? StudentId { get; init; }
        public bool? IsStandalone { get; init; }

        public bool IsStandaloneOrEnrolledInProgram { get; init; }
        public long? ExcludeStudentId { get; init; }

        public CourseQueryOptions() { }

        public CourseQueryOptions(CourseQueryOptions? other)
        {
            if (other is null)
                return;

            IncludeOrganization = other.IncludeOrganization;
            IncludeType = other.IncludeType;

            OrganizationId = other.OrganizationId;
            StudentId = other.StudentId;
            IsStandalone = other.IsStandalone;
            ExcludeStudentId = other.ExcludeStudentId;
        }
    }
}