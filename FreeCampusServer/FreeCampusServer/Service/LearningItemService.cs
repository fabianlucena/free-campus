using FreeCampusServer.Entities;
using FreeCampusServer.IRepository;
using FreeCampusServer.IServices;
using RFBaseServices.Services;

namespace FreeCampusServer.Service
{
    public class LearningItemService(ILearningItemRepository learningItemRepository)
        : CommonEntityService<LearningItem>(learningItemRepository),
        ILearningItemService
    {
    }
}
