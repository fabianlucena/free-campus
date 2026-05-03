using FreeCampusServer.Entities;
using FreeCampusServer.IRepository;
using RFBaseEF.Repositories;
using RFBaseEntities.QueryOptions;

namespace FreeCampusServer.Repository
{
    public class ProgramVersionXCourseVersionRepository(AppDbContext appContext)
        : CreatableEntityRepository<ProgramVersionXCourseVersion>(appContext),
        IProgramVersionXCourseVersionRepository
    {
        public override IQueryable<ProgramVersionXCourseVersion> CreateDBSet(BaseQueryOptions? options = null)
        {
            var queryable = base.CreateDBSet(options);

            return queryable;
        }
    }
}
