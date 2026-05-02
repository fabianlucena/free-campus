using FreeCampusServer.Entities;
using FreeCampusServer.IRepository;
using RFBaseEF.Repositories;
using RFBaseEntities.QueryOptions;

namespace FreeCampusServer.Repository
{
    public class CourseXProgramRepository(AppDbContext appContext)
        : CreatableEntityRepository<CourseXProgram>(appContext),
        ICourseXProgramRepository
    {
        public override IQueryable<CourseXProgram> CreateDBSet(BaseQueryOptions? options = null)
        {
            var queryable = base.CreateDBSet(options);

            return queryable;
        }
    }
}
