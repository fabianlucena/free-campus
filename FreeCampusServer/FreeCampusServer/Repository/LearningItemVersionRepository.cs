using FreeCampusServer.Entities;
using FreeCampusServer.IRepository;
using FreeCampusServer.QueryOptions;
using Microsoft.EntityFrameworkCore;
using RFBaseEF.Repositories;
using RFBaseEntities.QueryOptions;

namespace FreeCampusServer.Repository
{
    public class LearningItemVersionRepository(AppDbContext appContext)
        : CommonEntityRepository<LearningItemVersion>(appContext),
        ILearningItemVersionRepository
    {
        public override IQueryable<LearningItemVersion> CreateDBSet(BaseQueryOptions? options = null)
        {
            var queryable = base.CreateDBSet(options);

            if (options is LearningItemVersionQueryOptions learningItemVersionOptions)
            {
                if (learningItemVersionOptions.IncludeLearningItem)
                {
                    queryable = queryable.Include(pv => pv.LearningItem);
                }
            }

            return queryable;
        }
    }
}
