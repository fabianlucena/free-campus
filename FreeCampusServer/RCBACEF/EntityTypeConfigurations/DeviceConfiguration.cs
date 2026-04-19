using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RCBACEF.Models;

namespace RCBACEF.EntityTypeConfigurations
{
    public class DeviceConfiguration : BaseConfiguration<Device>
    {
        public override void Configure(EntityTypeBuilder<Device> entity)
        {
            base.Configure(entity);
        }
    }
}
