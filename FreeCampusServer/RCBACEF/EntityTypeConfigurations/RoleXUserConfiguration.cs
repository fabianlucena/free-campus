using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RCBACEF.Models;

namespace RCBACEF.EntityTypeConfigurations
{
    public class RoleXUserConfiguration<T>
        : SoftDeletableJoinConfiguration<T>
        where T : RoleXUser
    {
        public override void Configure(EntityTypeBuilder<T> entity)
        {
            base.Configure(entity);

            entity.HasOne(u => u.Role)
                  .WithMany()
                  .HasForeignKey(u => u.RoleId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(u => u.User)
                  .WithMany()
                  .HasForeignKey(u => u.UserId)
                  .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
