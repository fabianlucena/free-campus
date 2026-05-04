using RFBaseEntities.QueryOptions;

namespace FreeCampusServer.QueryOptions
{
    public sealed class CourseEnrollmentQueryOptions : CommonJoinQueryOptions
    {
        public bool IncludeCourse { get; init; }
        public bool IncludeCourseVersion { get; init; }
        public bool IncludeStudent { get; init; }

        public CourseEnrollmentQueryOptions() { }

        public CourseEnrollmentQueryOptions(CourseEnrollmentQueryOptions? options)
            : base(options)
        {
            if (options is null)
                return;

            IncludeCourse = options.IncludeCourse;
            IncludeCourseVersion = options.IncludeCourseVersion;
            IncludeStudent = options.IncludeStudent;
        }

        public override CourseEnrollmentQueryOptions Clone()
            => new(this);
    }
}
