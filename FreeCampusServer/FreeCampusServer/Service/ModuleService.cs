using FreeCampusServer.Entities;
using FreeCampusServer.IRepository;
using FreeCampusServer.IServices;
using FreeCampusServer.QueryOptions;
using RFBaseServices.Services;

namespace FreeCampusServer.Service
{
    public class ModuleService(IModuleRepository moduleRepository)
        : CommonEntityService<Module>(moduleRepository),
        IModuleService
    {
    }
}
