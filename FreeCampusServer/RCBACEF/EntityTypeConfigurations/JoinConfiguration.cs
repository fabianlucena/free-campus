using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RCBACEF.Models;

namespace RCBACEF.EntityTypeConfigurations
{
    public class JoinConfiguration<T>
        : BaseConfiguration<T>
        where T : Join
    {
        public override void Configure(EntityTypeBuilder<T> entity)
        {
            base.Configure(entity);
        }
    }
}
