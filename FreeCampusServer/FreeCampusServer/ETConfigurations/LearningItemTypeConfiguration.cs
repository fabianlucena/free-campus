using FreeCampusServer.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RFBaseEF.ETConfigurations;

namespace FreeCampusServer.ETConfigurations
{
    public class LearningItemTypeConfiguration : CommonEntityConfiguration<LearningItemType>
    {
        public override void Configure(EntityTypeBuilder<LearningItemType> entity)
        {
            base.Configure(entity);
        }
    }
}
