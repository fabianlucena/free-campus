using FreeCampusServer.Entities;
using FreeCampusServer.IRepository;
using FreeCampusServer.IServices;
using RFBaseServices.Services;
using RFL10n;

namespace FreeCampusServer.Service
{
    public class LearningItemTypeService(ILearningItemTypeRepository learningItemTypeRepository, IL10n l10n)
        : LocalizableEntityService<LearningItemType>(learningItemTypeRepository, l10n),
        ILearningItemTypeService
    {
    }
}
