using FreeCampusServer.Entities;
using FreeCampusServer.IRepository;
using FreeCampusServer.IServices;
using RFBaseServices.Services;

namespace FreeCampusServer.Service
{
    public class ProgramTypeService(IProgramTypeRepository programTypeRepository)
        : LocalizableEntityService<ProgramType>(programTypeRepository),
        IProgramTypeService
    {
    }
}
