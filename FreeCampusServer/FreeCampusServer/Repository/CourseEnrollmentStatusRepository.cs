using FreeCampusServer.Entities;
using FreeCampusServer.IRepository;
using FreeCampusServer.QueryOptions;
using Microsoft.EntityFrameworkCore;
using RFBaseEF.Repositories;
using RFBaseEntities.QueryOptions;

namespace FreeCampusServer.Repository
{
    public class CourseEnrollmentStatusRepository(AppDbContext appContext)
        : TranslatableEntityRepository<CourseEnrollmentStatus>(appContext),
        ICourseEnrollmentStatusRepository
    {
        public override IQueryable<CourseEnrollmentStatus> CreateDBSet(BaseQueryOptions? options = null)
        {
            var queryable = base.CreateDBSet(options);

            if (options is CourseEnrollmentStatusQueryOptions courseEnrollmentStatusOptions)
            {
                if (courseEnrollmentStatusOptions.IncludeOrganization)
                    queryable = queryable.Include(t => t.Organization);

                if (courseEnrollmentStatusOptions.OrganizationId.HasValue)
                    queryable = queryable.Where(t => t.OrganizationId == courseEnrollmentStatusOptions.OrganizationId.Value);
            }

            return queryable;
        }
    }
}
