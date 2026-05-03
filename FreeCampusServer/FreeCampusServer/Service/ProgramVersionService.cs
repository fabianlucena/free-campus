using FreeCampusServer.Entities;
using FreeCampusServer.IRepository;
using FreeCampusServer.IServices;
using RFBaseServices.Services;

namespace FreeCampusServer.Service
{
    public class ProgramVersionService(IProgramVersionRepository programVersionRepository)
        : CommonEntityService<ProgramVersion>(programVersionRepository),
        IProgramVersionService
    {
    }
}
