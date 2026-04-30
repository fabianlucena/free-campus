using FreeCampusServer.Entities;
using FreeCampusServer.IRepository;
using FreeCampusServer.QueryOptions;
using Microsoft.EntityFrameworkCore;
using RFBaseEF.Repositories;
using RFBaseEntities.QueryOptions;

namespace FreeCampusServer.Repository
{
    public class ModuleTypeRepository
        : CreatableEntityRepository<ModuleType>,
        IModuleTypeRepository
    {
        public ModuleTypeRepository(DbContext context) : base(context) { }

        public override IQueryable<ModuleType> CreateDBSet(BaseQueryOptions? options)
        {
            var queryable = base.CreateDBSet(options ?? new BaseQueryOptions());

            if (options is ModuleTypeQueryOptions moduleTypeOptions)
            {
                if (moduleTypeOptions.IncludeOrganization)
                {
                    queryable = queryable.Include(t => t.Organization);
                }
            }

            return queryable;
        }
    }
}
