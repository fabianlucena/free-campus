using FreeCampusServer.Entities;
using FreeCampusServer.IRepository;
using FreeCampusServer.IServices;
using FreeCampusServer.QueryOptions;
using RFBaseServices.Services;

namespace FreeCampusServer.Service
{
    public class ProgramTypeService(IProgramTypeRepository programTypeRepository)
        : CommonEntityService<ProgramType>(programTypeRepository),
        IProgramTypeService
    {
    }
}
