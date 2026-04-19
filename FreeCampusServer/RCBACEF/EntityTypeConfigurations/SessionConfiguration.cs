using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RCBACEF.Models;

namespace RCBACEF.EntityTypeConfigurations
{
    public class SessionConfiguration : BaseConfiguration<Session>
    {
        public override void Configure(EntityTypeBuilder<Session> entity)
        {
            base.Configure(entity);
        }
    }
}
