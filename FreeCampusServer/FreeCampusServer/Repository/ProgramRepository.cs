using FreeCampusServer.IRepository;
using FreeCampusServer.QueryOptions;
using Microsoft.EntityFrameworkCore;
using RFBaseEF.Repositories;
using RFBaseEntities.QueryOptions;

namespace FreeCampusServer.Repository
{
    public class ProgramRepository
        : CreatableEntityRepository<Entities.Program>,
        IProgramRepository
    {
        public ProgramRepository(DbContext context) : base(context) { }

        public override IQueryable<Entities.Program> CreateDBSet(BaseQueryOptions? options)
        {
            var quereable = base.CreateDBSet(options ?? new BaseQueryOptions());

            if (options is ProgramQueryOptions programOptions)
            {
                if (programOptions.IncludeType)
                {
                    quereable = quereable.Include(p => p.Type);
                }

                if (programOptions.IncludeOrganization)
                {
                    quereable = quereable.Include(p => p.Organization);
                }
            }

            return quereable;
        }
    }
}
