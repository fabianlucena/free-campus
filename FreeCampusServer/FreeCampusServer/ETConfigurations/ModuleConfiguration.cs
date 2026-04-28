using FreeCampusServer.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RFBaseEF.ETConfigurations;

namespace FreeCampusServer.ETConfigurations
{
    public class ModuleConfiguration : BaseConfiguration<Module>
    {
        public override void Configure(EntityTypeBuilder<Module> entity)
        {
            base.Configure(entity);
        }
    }
}
