using FreeCampusServer.Entities;
using FreeCampusServer.IRepository;
using FreeCampusServer.IServices;
using RFBaseServices.Services;
using RFL10n;

namespace FreeCampusServer.Service
{
    public class ModuleTypeService(IModuleTypeRepository moduleTypeRepository, IL10n l10n)
        : LocalizableEntityService<ModuleType>(moduleTypeRepository, l10n),
        IModuleTypeService
    {
    }
}
