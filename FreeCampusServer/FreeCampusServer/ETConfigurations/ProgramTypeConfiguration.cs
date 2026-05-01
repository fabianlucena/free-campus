using FreeCampusServer.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RFBaseEF.ETConfigurations;

namespace FreeCampusServer.ETConfigurations
{
    public class ProgramTypeConfiguration : CommonEntityConfiguration<ProgramType>
    {
        public override void Configure(EntityTypeBuilder<ProgramType> entity)
        {
            base.Configure(entity);
        }
    }
}
