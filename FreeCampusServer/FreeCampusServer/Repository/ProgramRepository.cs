using FreeCampusServer.IRepository;
using FreeCampusServer.QueryOptions;
using Microsoft.EntityFrameworkCore;
using RFBaseEF.Repositories;
using RFBaseEntities.QueryOptions;
using RFRGOBACEntities.Entities;

namespace FreeCampusServer.Repository
{
    public class ProgramRepository
        : CreatableEntityRepository<Entities.Program>,
        IProgramRepository
    {
        public ProgramRepository(DbContext context) : base(context) { }

        public override IQueryable<Entities.Program> CreateDBSet(BaseQueryOptions? options)
        {
            var queryable = base.CreateDBSet(options ?? new BaseQueryOptions());

            if (options is ProgramQueryOptions programOptions)
            {
                if (programOptions.IncludeOrganization)
                    queryable = queryable.Include(p => p.Organization);

                if (programOptions.IncludeType)
                    queryable = queryable.Include(p => p.Type);

                if (programOptions.OrganizationId is not null)
                    queryable = queryable.Where(p => p.OrganizationId == programOptions.OrganizationId);
            }

            return queryable;
        }

        public async Task<IEnumerable<long>> GetIdListAsync(ProgramQueryOptions options)
        {
            var queryable = CreateDBSet(options);
            if (options.OrganizationId is not null)
                queryable = queryable.Where(p => p.OrganizationId == options.OrganizationId);

            var idList = await queryable
                .Select(p => p.Id)
                .Skip(options.Skip)
                .Take(options.Take)
                .ToListAsync();

            return idList;
        }
    }
}
