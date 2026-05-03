using FreeCampusServer.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RFBaseEF.ETConfigurations;

namespace FreeCampusServer.ETConfigurations
{
    public class CourseVersionConfiguration : CommonEntityConfiguration<CourseVersion>
    {
        public override void Configure(EntityTypeBuilder<CourseVersion> entity)
        {
            base.Configure(entity);
        }
    }
}
