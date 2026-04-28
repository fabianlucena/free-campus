using FreeCampusServer.Entities;
using FreeCampusServer.IRepository;
using FreeCampusServer.QueryOptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RFAuthEntities.Entities;
using RFAuthEntities.QueryOptions;
using RFBaseEF.Repositories;
using RFBaseEntities.QueryOptions;
using RFRBACEntities.QueryOptions;

namespace FreeCampusServer.Repository
{
    public class CourseTypeRepository
        : CreatableEntityRepository<CourseType>,
        ICourseTypeRepository
    {
        public CourseTypeRepository(DbContext context) : base(context) { }

        public override IQueryable<CourseType> CreateDBSet(BaseQueryOptions? options)
        {
            var quereable = base.CreateDBSet(options ?? new BaseQueryOptions());

            return quereable;
        }
    }
}
