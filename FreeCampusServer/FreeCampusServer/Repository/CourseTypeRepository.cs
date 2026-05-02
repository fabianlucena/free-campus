using FreeCampusServer.Entities;
using FreeCampusServer.IRepository;
using FreeCampusServer.QueryOptions;
using Microsoft.EntityFrameworkCore;
using RFBaseEF.Repositories;
using RFBaseEntities.QueryOptions;

namespace FreeCampusServer.Repository
{
    public class CourseTypeRepository(AppDbContext appContext)
        : LocalizableEntityRepository<CourseType>(appContext),
        ICourseTypeRepository
    {
        public override IQueryable<CourseType> CreateDBSet(BaseQueryOptions? options = null)
        {
            var queryable = base.CreateDBSet(options);

            if (options is CourseTypeQueryOptions courseTypeOptions)
            {
                if (courseTypeOptions.IncludeOrganization)
                {
                    queryable = queryable.Include(t => t.Organization);
                }
            }

            return queryable;
        }

        public async Task<IEnumerable<CourseType>> GetListByOrganizationIdAsync(long organizationId, CourseTypeQueryOptions? options = null)
        {
            var query = GetDBSet(options);
            var result = await query
                .Where(t => t.OrganizationId == organizationId)
                .ToListAsync();

            return result;
        }
    }
}
