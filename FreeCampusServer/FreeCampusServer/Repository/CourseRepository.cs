using FreeCampusServer.Entities;
using FreeCampusServer.IRepository;
using FreeCampusServer.QueryOptions;
using Microsoft.EntityFrameworkCore;
using RFBaseEF.Repositories;
using RFBaseEntities.QueryOptions;

namespace FreeCampusServer.Repository
{
    public class CourseRepository(DbContext context)
        : CreatableEntityRepository<Course>(context),
        ICourseRepository
    {
        public override IQueryable<Course> CreateDBSet(BaseQueryOptions? options)
        {
            var queryable = base.CreateDBSet(options);

            if (options is CourseQueryOptions courseOptions)
            {
                if (courseOptions.IncludeOrganization)
                    queryable = queryable.Include(c => c.Organization);

                if (courseOptions.IncludeType)
                    queryable = queryable.Include(c => c.Type);

                if (courseOptions.OrganizationId is not null)
                    queryable = queryable.Where(c => c.OrganizationId == courseOptions.OrganizationId);

                if (courseOptions.IsStandalone is not null)
                    queryable = queryable.Where(c => c.IsStandalone == courseOptions.IsStandalone);

                if (courseOptions.Ids is not null)
                    queryable = queryable.Where(c => courseOptions.Ids.Contains(c.Id));

                if (courseOptions.ExcludeIds is not null)
                    queryable = queryable.Where(c => !courseOptions.ExcludeIds.Contains(c.Id));

                if (courseOptions.StudentId is not null || courseOptions.ExcludeStudentId is not null)
                {
                    var courseEnrollmentRepository = new CourseEnrollmentRepository(context);
                    var ceDB = courseEnrollmentRepository.CreateDBSet();
                    if (courseOptions.StudentId is not null)
                        queryable.Where(c => ceDB.Any(ce => ce.CourseId == c.Id && ce.StudentId == courseOptions.StudentId));

                    if (courseOptions.ExcludeStudentId is not null)
                        queryable.Where(c => !ceDB.Any(ce => ce.CourseId == c.Id && ce.StudentId == courseOptions.ExcludeStudentId));
                }
            }

            return queryable;
        }

        public async Task<IEnumerable<Course>> GetStandaloneListByOrganizationIdAsync(long organizationId, CourseQueryOptions? options = null)
        {
            var set = CreateDBSet(options);
            var session = await set
                .Where(c => c.OrganizationId == organizationId
                    && c.IsStandalone
                )
                .ToListAsync();

            return session;
        }
    }
}
