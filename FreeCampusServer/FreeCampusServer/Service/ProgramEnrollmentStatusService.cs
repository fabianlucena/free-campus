using FreeCampusServer.Entities;
using FreeCampusServer.IRepository;
using FreeCampusServer.IServices;
using FreeCampusServer.QueryOptions;
using RFBaseServices.Services;

namespace FreeCampusServer.Service
{
    public class ProgramEnrollmentStatusService(IProgramEnrollmentStatusRepository programEnrollmentStatusRepository)
        : TranslatableEntityService<ProgramEnrollmentStatus>(programEnrollmentStatusRepository),
        IProgramEnrollmentStatusService
    {
        public async Task<IEnumerable<ProgramEnrollmentStatus>> GetListByOrganizationIdAsync(long organizationId, ProgramEnrollmentStatusQueryOptions? options = null)
             => await programEnrollmentStatusRepository.GetListAsync(new ProgramEnrollmentStatusQueryOptions(options)
            {
                OrganizationId = organizationId,
            });
    }
}
