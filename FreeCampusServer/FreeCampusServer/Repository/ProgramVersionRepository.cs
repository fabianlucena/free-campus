using FreeCampusServer.Entities;
using FreeCampusServer.IRepository;
using FreeCampusServer.QueryOptions;
using Microsoft.EntityFrameworkCore;
using RFBaseEF.Repositories;
using RFBaseEntities.QueryOptions;

namespace FreeCampusServer.Repository
{
    public class ProgramVersionRepository(AppDbContext appContext)
        : CommonEntityRepository<ProgramVersion>(appContext),
        IProgramVersionRepository
    {
        public override IQueryable<ProgramVersion> CreateDBSet(BaseQueryOptions? options = null)
        {
            var queryable = base.CreateDBSet(options);

            if (options is ProgramVersionQueryOptions programVersionOptions)
            {
                if (programVersionOptions.IncludeProgram)
                {
                    queryable = queryable.Include(pv => pv.Program);
                }
            }

            return queryable;
        }
    }
}
