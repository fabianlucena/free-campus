using FreeCampusServer.Entities;
using FreeCampusServer.IRepository;
using FreeCampusServer.IServices;
using RFBaseServices.Services;

namespace FreeCampusServer.Service
{
    public class LearningItemVersionService(ILearningItemVersionRepository learningItemVersionRepository)
        : CommonEntityService<LearningItemVersion>(learningItemVersionRepository),
        ILearningItemVersionService
    {
    }
}
