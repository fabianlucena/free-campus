using RFBaseEntities.QueryOptions;

namespace FreeCampusServer.QueryOptions
{
    public class CourseQueryOptions : CommonEntityQueryOptions
    {
        public bool Translate { get; init; }

        public bool IncludeOrganization { get; init; }
        public bool IncludeType { get; init; } = true;

        public long? OrganizationId { get; init; }
        public long? StudentId { get; init; }
        public bool? IsStandalone { get; init; }

        public bool IsStandaloneOrEnrolledInProgram { get; init; }
        public long? ExcludeStudentId { get; init; }

        public CourseQueryOptions() { }

        public CourseQueryOptions(CourseQueryOptions options)
        {
            Translate = options.Translate;

            IncludeOrganization = options.IncludeOrganization;
            IncludeType = options.IncludeType;

            OrganizationId = options.OrganizationId;
            StudentId = options.StudentId;
            IsStandalone = options.IsStandalone;
            IsStandaloneOrEnrolledInProgram = options.IsStandaloneOrEnrolledInProgram;
            ExcludeStudentId = options.ExcludeStudentId;
        }
    }
}