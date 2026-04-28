using FreeCampusServer.IRepository;
using FreeCampusServer.IServices;
using FreeCampusServer.QueryOptions;
using RFBaseServices.Services;

namespace FreeCampusServer.Service
{
    public class ProgramService(IProgramRepository programRepository)
        : CommonEntityService<Entities.Program>(programRepository),
        IProgramService
    {
    }
}
