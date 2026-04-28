using FreeCampusServer.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RFBaseEF.ETConfigurations;

namespace FreeCampusServer.ETConfigurations
{
    public class CourseConfiguration : BaseConfiguration<Course>
    {
        public override void Configure(EntityTypeBuilder<Course> entity)
        {
            base.Configure(entity);
        }
    }
}
