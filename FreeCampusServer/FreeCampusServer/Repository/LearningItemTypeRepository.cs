using FreeCampusServer.Entities;
using FreeCampusServer.IRepository;
using FreeCampusServer.QueryOptions;
using Microsoft.EntityFrameworkCore;
using RFBaseEF.Repositories;
using RFBaseEntities.QueryOptions;

namespace FreeCampusServer.Repository
{
    public class LearningItemTypeRepository(AppDbContext appContext)
        : LocalizableEntityRepository<LearningItemType>(appContext),
        ILearningItemTypeRepository
    {
        public override IQueryable<LearningItemType> CreateDBSet(BaseQueryOptions? options = null)
        {
            var queryable = base.CreateDBSet(options);

            if (options is LearningItemTypeQueryOptions learningItemTypeOptions)
            {
                if (learningItemTypeOptions.IncludeOrganization)
                {
                    queryable = queryable.Include(t => t.Organization);
                }
            }

            return queryable;
        }
    }
}
