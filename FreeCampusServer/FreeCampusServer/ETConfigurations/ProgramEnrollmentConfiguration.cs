using FreeCampusServer.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RFBaseEF.ETConfigurations;

namespace FreeCampusServer.ETConfigurations
{
    public class ProgramEnrollmentConfiguration : CommonEntityConfiguration<ProgramEnrollment>
    {
        public override void Configure(EntityTypeBuilder<ProgramEnrollment> entity)
        {
            base.Configure(entity);

            entity.Property(e => e.FinalGrade)
                .HasPrecision(5, 2);
        }
    }
}
