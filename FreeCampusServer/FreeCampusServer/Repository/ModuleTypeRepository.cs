using FreeCampusServer.Entities;
using FreeCampusServer.IRepository;
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
            var quereable = base.CreateDBSet(options ?? new BaseQueryOptions());

            return quereable;
        }
    }
}
