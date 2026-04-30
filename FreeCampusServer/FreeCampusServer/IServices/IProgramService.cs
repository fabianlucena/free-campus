using FreeCampusServer.Entities;
using FreeCampusServer.QueryOptions;
using RFBaseIServices.IServices;

namespace FreeCampusServer.IServices
{
    public interface IProgramService : ICommonEntityService<Entities.Program>
    {
        Task<IEnumerable<long>> GetIdListByOrganizationIdAsync(long organizationId, ProgramQueryOptions? options = null);
        Task<IEnumerable<Course>> GetCoursesByOrganizationIdAsync(long organizationId, ProgramQueryOptions? options = null);
    }
}
