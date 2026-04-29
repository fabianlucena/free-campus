using FreeCampusServer.Entities;
using FreeCampusServer.IRepository;
using FreeCampusServer.QueryOptions;
using Microsoft.EntityFrameworkCore;
using RFBaseEF.Repositories;
using RFBaseEntities.QueryOptions;

namespace FreeCampusServer.Repository
{
    public class ProgramTypeRepository
        : CreatableEntityRepository<ProgramType>,
        IProgramTypeRepository
    {
        public ProgramTypeRepository(DbContext context) : base(context) { }

        public override IQueryable<ProgramType> CreateDBSet(BaseQueryOptions? options)
        {
            var quereable = base.CreateDBSet(options ?? new BaseQueryOptions());

            if (options is ProgramTypeQueryOptions programTypeOptions)
            {
                if (programTypeOptions.IncludeOrganization)
                {
                    quereable = quereable.Include(t => t.Organization);
                }
            }

            return quereable;
        }
    }
}
