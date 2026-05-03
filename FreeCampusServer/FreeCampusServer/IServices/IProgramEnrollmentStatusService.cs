using FreeCampusServer.Entities;
using FreeCampusServer.QueryOptions;
using RFBaseIServices.IServices;

namespace FreeCampusServer.IServices
{
    public interface IProgramEnrollmentStatusService : ITranslatableEntityService<ProgramEnrollmentStatus>
    {
        Task<IEnumerable<ProgramEnrollmentStatus>> GetListByOrganizationIdAsync(long organizationId, ProgramEnrollmentStatusQueryOptions? options = null);
    }
}
