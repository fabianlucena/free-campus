using FreeCampusServer.Entities;
using FreeCampusServer.IRepository;
using FreeCampusServer.IServices;
using RFBaseServices.Services;
using RFL10n;

namespace FreeCampusServer.Service
{
    public class ProgramTypeService(IProgramTypeRepository programTypeRepository, IL10n l10n)
        : LocalizableEntityService<ProgramType>(programTypeRepository, l10n),
        IProgramTypeService
    {
    }
}
