using FreeCampusServer.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RFBaseEF.ETConfigurations;

namespace FreeCampusServer.ETConfigurations
{
    public class ModuleTypeConfiguration : CommonEntityConfiguration<ModuleType>
    {
        public override void Configure(EntityTypeBuilder<ModuleType> entity)
        {
            base.Configure(entity);
        }
    }
}
