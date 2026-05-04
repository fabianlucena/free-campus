using RFBaseEntities.QueryOptions;

namespace FreeCampusServer.QueryOptions
{
    public class ProgramEnrollmentQueryOptions : CommonJoinQueryOptions
    {
        public bool IncludeProgram { get; init; }
        public bool IncludeProgramVersion { get; init; }
        public bool IncludeStudent { get; init; }

        public ProgramEnrollmentQueryOptions() { }

        public ProgramEnrollmentQueryOptions(ProgramEnrollmentQueryOptions? options)
            : base(options)
        {
            if (options == null)
                return;

            IncludeProgram = options.IncludeProgram;
            IncludeProgramVersion = options.IncludeProgramVersion;
            IncludeStudent = options.IncludeStudent;
        }

        public override ProgramEnrollmentQueryOptions Clone()
            => new(this);
    }
}
