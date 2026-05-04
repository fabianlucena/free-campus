using RFBaseEntities.QueryOptions;

namespace FreeCampusServer.QueryOptions
{
    public sealed class ProgramEnrollmentStatusQueryOptions : NominableEntityQueryOptions
    {
        public bool IncludeOrganization { get; init; }

        public long? OrganizationId { get; init; }

        public ProgramEnrollmentStatusQueryOptions() { }

        public ProgramEnrollmentStatusQueryOptions(ProgramEnrollmentStatusQueryOptions? options)
            : base(options)
        {
            if (options == null)
                return;

            IncludeOrganization = options.IncludeOrganization;
            OrganizationId = options.OrganizationId;
        }

        public override ProgramEnrollmentStatusQueryOptions Clone()
            => new(this);
    }
}
