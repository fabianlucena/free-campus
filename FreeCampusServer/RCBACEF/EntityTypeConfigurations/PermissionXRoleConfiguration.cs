using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RCBACEF.Models;

namespace RCBACEF.EntityTypeConfigurations
{
    public class PermissionXRoleConfiguration
        : SoftDeletableJoinConfiguration<PermissionXRole>
    {
        public override void Configure(EntityTypeBuilder<PermissionXRole> entity)
        {
            base.Configure(entity);

            entity.HasOne(u => u.Permission)
                  .WithMany()
                  .HasForeignKey(u => u.PermissionId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(u => u.Role)
                  .WithMany()
                  .HasForeignKey(u => u.RoleId)
                  .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
