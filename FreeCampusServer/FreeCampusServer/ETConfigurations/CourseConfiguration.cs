using FreeCampusServer.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RFBaseEF.ETConfigurations;

namespace FreeCampusServer.ETConfigurations
{
    public class CourseTypeConfiguration : BaseConfiguration<CourseType>
    {
        public override void Configure(EntityTypeBuilder<CourseType> entity)
        {
            base.Configure(entity);
        }
    }
}
