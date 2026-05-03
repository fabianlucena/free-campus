using RFBaseEntities.QueryOptions;

namespace FreeCampusServer.QueryOptions
{
    public class ProgramEnrollmentStatusQueryOptions : NominableEntityQueryOptions
    {
        public ProgramEnrollmentStatusQueryOptions(NominableEntityQueryOptions? options) : base(options)
        {
        }

        public bool IncludeOrganization { get; init; }

        public long ? OrganizationId { get; init; }
    }
}
