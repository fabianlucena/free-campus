using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RCBACEF.Models;

namespace RCBACEF.EntityTypeConfigurations
{
    public class RoleIncludeConfiguration
        : SoftDeletableJoinConfiguration<RoleInclude>
    {
        public override void Configure(EntityTypeBuilder<RoleInclude> entity)
        {
            base.Configure(entity);

            entity.HasOne(u => u.Role)
                  .WithMany()
                  .HasForeignKey(u => u.RoleId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(u => u.Include)
                  .WithMany()
                  .HasForeignKey(u => u.IncludeId)
                  .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
