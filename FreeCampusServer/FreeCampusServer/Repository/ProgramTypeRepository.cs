using FreeCampusServer.Entities;
using FreeCampusServer.IRepository;
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

            return quereable;
        }
    }
}
