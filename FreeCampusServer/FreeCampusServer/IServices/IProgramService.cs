using FreeCampusServer.Entities;
using FreeCampusServer.QueryOptions;
using RFBaseIServices.IServices;

namespace FreeCampusServer.IServices
{
    public interface IProgramService : ICommonEntityService<Entities.Program>
    {
        Task<IEnumerable<long>> GetIdListAsync(ProgramQueryOptions options);
        Task<IEnumerable<Course>> GetAvailableCoursesAsync(ProgramQueryOptions options);
    }
}
