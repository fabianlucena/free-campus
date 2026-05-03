using FreeCampusServer.Entities;
using FreeCampusServer.IRepository;
using FreeCampusServer.QueryOptions;
using Microsoft.EntityFrameworkCore;
using RFBaseEF.Repositories;
using RFBaseEntities.QueryOptions;

namespace FreeCampusServer.Repository
{
    public class CourseVersionRepository(AppDbContext appContext)
        : CommonEntityRepository<CourseVersion>(appContext),
        ICourseVersionRepository
    {
        public override IQueryable<CourseVersion> CreateDBSet(BaseQueryOptions? options = null)
        {
            var queryable = base.CreateDBSet(options);

            if (options is CourseVersionQueryOptions courseVersionOptions)
            {
                if (courseVersionOptions.IncludeCourse)
                {
                    queryable = queryable.Include(pv => pv.Course);
                }
            }

            return queryable;
        }
    }
}
