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
            var quereable = base.CreateDBSet(options ?? new BaseQueryOptions());

            if (options is ModuleQueryOptions moduleOptions)
            {
                if (moduleOptions.IncludeOrganization)
                {
                    quereable = quereable.Include(m => m.Organization);
                }

                if (moduleOptions.IncludeType)
                {
                    quereable = quereable.Include(m => m.Type);
                }
            }

            return quereable;
        }
    }
}
