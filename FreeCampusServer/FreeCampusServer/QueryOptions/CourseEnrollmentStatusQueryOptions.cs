using RFBaseEntities.QueryOptions;

namespace FreeCampusServer.QueryOptions
{
    public class CourseEnrollmentStatusQueryOptions : NominableEntityQueryOptions
    {
        public bool IncludeOrganization { get; init; }

        public long? OrganizationId { get; init; }

        public CourseEnrollmentStatusQueryOptions() { }

        public CourseEnrollmentStatusQueryOptions(CourseEnrollmentStatusQueryOptions? options)
            : base(options)
        {
            if (options == null)
                return;

            IncludeOrganization = options.IncludeOrganization;
            OrganizationId = options.OrganizationId;
        }

        public override CourseEnrollmentStatusQueryOptions Clone()
            => new(this);
    }
}
