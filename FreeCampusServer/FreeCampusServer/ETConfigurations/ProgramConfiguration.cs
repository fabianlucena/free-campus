using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RFBaseEF.ETConfigurations;

namespace FreeCampusServer.ETConfigurations
{
    public class ProgramConfiguration : BaseConfiguration<Entities.Program>
    {
        public override void Configure(EntityTypeBuilder<Entities.Program> entity)
        {
            base.Configure(entity);
        }
    }
}
