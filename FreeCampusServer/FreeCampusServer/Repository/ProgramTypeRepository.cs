using FreeCampusServer.Entities;
using FreeCampusServer.IRepository;
using FreeCampusServer.QueryOptions;
using Microsoft.EntityFrameworkCore;
using RFBaseEF.Repositories;
using RFBaseEntities.QueryOptions;

namespace FreeCampusServer.Repository
{
    public class ProgramTypeRepository(AppDbContext appContext)
        : CreatableEntityRepository<ProgramType>(appContext),
        IProgramTypeRepository
    {
        public override IQueryable<ProgramType> CreateDBSet(BaseQueryOptions? options)
        {
            var queryable = base.CreateDBSet(options ?? new BaseQueryOptions());

            if (options is ProgramTypeQueryOptions programTypeOptions)
            {
                if (programTypeOptions.IncludeOrganization)
                {
                    queryable = queryable.Include(t => t.Organization);
                }
            }

            return queryable;
        }
    }
}
