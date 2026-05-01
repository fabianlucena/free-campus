using FreeCampusServer.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RFBaseEF.ETConfigurations;

namespace FreeCampusServer.ETConfigurations
{
    public class CourseEnrollmentConfiguration : CommonEntityConfiguration<CourseEnrollment>
    {
        public override void Configure(EntityTypeBuilder<CourseEnrollment> entity)
        {
            base.Configure(entity);
        }
    }
}
