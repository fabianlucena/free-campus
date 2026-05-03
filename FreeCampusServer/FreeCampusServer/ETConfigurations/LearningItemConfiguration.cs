using FreeCampusServer.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RFBaseEF.ETConfigurations;

namespace FreeCampusServer.ETConfigurations
{
    public class LearningItemConfiguration : CommonEntityConfiguration<LearningItem>
    {
        public override void Configure(EntityTypeBuilder<LearningItem> entity)
        {
            base.Configure(entity);
        }
    }
}
