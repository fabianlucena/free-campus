using FreeCampusServer.Entities;
using FreeCampusServer.IRepository;
using FreeCampusServer.QueryOptions;
using Microsoft.EntityFrameworkCore;
using RFBaseEF.Repositories;
using RFBaseEntities.QueryOptions;

namespace FreeCampusServer.Repository
{
    public class CourseStatusRepository(AppDbContext appContext)
        : NominableEntityRepository<CourseStatus>(appContext),
        ICourseStatusRepository
    {
        public override IQueryable<CourseStatus> CreateDBSet(BaseQueryOptions? options = null)
        {
            var queryable = base.CreateDBSet(options ?? new BaseQueryOptions());

            if (options is CourseStatusQueryOptions courseStatusOptions)
            {
                if (courseStatusOptions.IncludeOrganization)
                {
                    queryable = queryable.Include(t => t.Organization);
                }
            }

            return queryable;
        }

        public async Task<IEnumerable<CourseStatus>> GetListByOrganizationIdAsync(long organizationId, CourseStatusQueryOptions? options = null)
        {
            var query = CreateDBSet(options);
            var result = await query
                .Where(t => t.OrganizationId == organizationId)
                .ToListAsync();

            return result;
        }
    }
}
