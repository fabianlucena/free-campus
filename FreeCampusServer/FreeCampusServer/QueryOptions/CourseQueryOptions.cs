using RFBaseEntities.QueryOptions;

namespace FreeCampusServer.QueryOptions
{
    public sealed class CourseQueryOptions : CommonEntityQueryOptions
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

        public CourseQueryOptions(CourseQueryOptions? options)
            : base(options)
        {
            if (options == null)
                return;

            Translate = options.Translate;

            IncludeOrganization = options.IncludeOrganization;
            IncludeType = options.IncludeType;

            OrganizationId = options.OrganizationId;
            StudentId = options.StudentId;
            IsStandalone = options.IsStandalone;
            IsStandaloneOrEnrolledInProgram = options.IsStandaloneOrEnrolledInProgram;
            ExcludeStudentId = options.ExcludeStudentId;
        }

        public override CourseQueryOptions Clone()
            => new(this);
    }
}