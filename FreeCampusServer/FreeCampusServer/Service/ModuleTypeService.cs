using FreeCampusServer.Entities;
using FreeCampusServer.IRepository;
using FreeCampusServer.IServices;
using RFBaseServices.Services;

namespace FreeCampusServer.Service
{
    public class ModuleTypeService(IModuleTypeRepository moduleTypeRepository)
        : LocalizableEntityService<ModuleType>(moduleTypeRepository),
        IModuleTypeService
    {
    }
}
