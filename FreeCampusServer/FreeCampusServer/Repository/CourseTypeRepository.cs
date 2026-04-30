using FreeCampusServer.Entities;
using FreeCampusServer.IRepository;
using FreeCampusServer.QueryOptions;
using Microsoft.EntityFrameworkCore;
using RFBaseEF.Repositories;
using RFBaseEntities.QueryOptions;

namespace FreeCampusServer.Repository
{
    public class CourseTypeRepository
        : CreatableEntityRepository<CourseType>,
        ICourseTypeRepository
    {
        public CourseTypeRepository(DbContext context) : base(context) { }

        public override IQueryable<CourseType> CreateDBSet(BaseQueryOptions? options)
        {
            var queryable = base.CreateDBSet(options ?? new BaseQueryOptions());

            if (options is CourseTypeQueryOptions courseTypeOptions)
            {
                if (courseTypeOptions.IncludeOrganization)
                {
                    queryable = queryable.Include(t => t.Organization);
                }
            }

            return queryable;
        }
    }
}
