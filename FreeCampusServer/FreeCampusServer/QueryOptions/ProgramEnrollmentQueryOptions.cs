using RFBaseEntities.QueryOptions;

namespace FreeCampusServer.QueryOptions
{
    public class ProgramEnrollmentQueryOptions : CommonJoinQueryOptions
    {
        public bool IncludeProgram { get; init; }
        public bool IncludeStudent { get; init; }

        public long? StudentId { get; init; }
    }
}
