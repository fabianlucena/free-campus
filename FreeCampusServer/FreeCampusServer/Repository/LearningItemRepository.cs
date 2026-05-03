using FreeCampusServer.Entities;
using FreeCampusServer.IRepository;
using FreeCampusServer.QueryOptions;
using Microsoft.EntityFrameworkCore;
using RFBaseEF.Repositories;
using RFBaseEntities.QueryOptions;

namespace FreeCampusServer.Repository
{
    public class LearningItemRepository(AppDbContext appContext)
        : CreatableEntityRepository<LearningItem>(appContext),
        ILearningItemRepository
    {
        public override IQueryable<LearningItem> CreateDBSet(BaseQueryOptions? options = null)
        {
            var queryable = base.CreateDBSet(options);

            if (options is LearningItemQueryOptions learningItemOptions)
            {
                if (learningItemOptions.IncludeOrganization)
                {
                    queryable = queryable.Include(l => l.Organization);
                }

                if (learningItemOptions.IncludeType)
                {
                    queryable = queryable.Include(l => l.Type);
                }
            }

            return queryable;
        }
    }
}
