using FreeCampusServer.Entities;
using FreeCampusServer.IRepository;
using FreeCampusServer.QueryOptions;
using Microsoft.EntityFrameworkCore;
using RFBaseEF.Repositories;
using RFBaseEntities.QueryOptions;

namespace FreeCampusServer.Repository
{
    public class CourseEnrollmentRepository(DbContext appContext)
        : CreatableEntityRepository<CourseEnrollment>(appContext),
        ICourseEnrollmentRepository
    {
        public override IQueryable<CourseEnrollment> CreateDBSet(BaseQueryOptions? options = null)
        {
            var queryable = base.CreateDBSet(options);

            if (options is CourseEnrollmentQueryOptions courseEnrollmentOptions)
            {
                if (courseEnrollmentOptions.IncludeCourse)
                    queryable = queryable.Include(ce => ce.Course);

                if (courseEnrollmentOptions.IncludeStudent)
                    queryable = queryable.Include(ce => ce.Student);
            }

            return queryable;
        }
    }
}
