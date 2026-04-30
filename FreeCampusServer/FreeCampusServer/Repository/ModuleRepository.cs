using FreeCampusServer.Entities;
using FreeCampusServer.IRepository;
using FreeCampusServer.QueryOptions;
using Microsoft.EntityFrameworkCore;
using RFBaseEF.Repositories;
using RFBaseEntities.QueryOptions;

namespace FreeCampusServer.Repository
{
    public class ModuleRepository
        : CreatableEntityRepository<Module>,
        IModuleRepository
    {
        public ModuleRepository(DbContext context) : base(context) { }

        public override IQueryable<Module> CreateDBSet(BaseQueryOptions? options)
        {
            var queryable = base.CreateDBSet(options ?? new BaseQueryOptions());

            if (options is ModuleQueryOptions moduleOptions)
            {
                if (moduleOptions.IncludeOrganization)
                {
                    queryable = queryable.Include(m => m.Organization);
                }

                if (moduleOptions.IncludeType)
                {
                    queryable = queryable.Include(m => m.Type);
                }
            }

            return queryable;
        }
    }
}
