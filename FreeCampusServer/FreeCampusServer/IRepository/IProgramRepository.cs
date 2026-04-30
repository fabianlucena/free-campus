using FreeCampusServer.QueryOptions;
using RFBaseIRepositories.IRepositories;

namespace FreeCampusServer.IRepository
{
    public interface IProgramRepository : ICommonEntityRepository<Entities.Program>
    {
        Task<IEnumerable<long>> GetIdListAsync(ProgramQueryOptions options);
    }
}