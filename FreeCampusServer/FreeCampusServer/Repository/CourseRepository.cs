using FreeCampusServer.Entities;
using FreeCampusServer.IRepository;
using FreeCampusServer.QueryOptions;
using Microsoft.EntityFrameworkCore;
using RFBaseEF.Repositories;
using RFBaseEntities.QueryOptions;

namespace FreeCampusServer.Repository
{
    public class CourseRepository
        : CreatableEntityRepository<Course>,
        ICourseRepository
    {
        public CourseRepository(DbContext context) : base(context) { }

        public override IQueryable<Course> CreateDBSet(BaseQueryOptions? options)
        {
            var quereable = base.CreateDBSet(options ?? new BaseQueryOptions());

            if (options is CourseQueryOptions courseOptions)
            {
                if (courseOptions.IncludeOrganization)
                {
                    quereable = quereable.Include(c => c.Organization);
                }

                if (courseOptions.IncludeType)
                {
                    quereable = quereable.Include(c => c.Type);
                }
            }

            return quereable;
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
