using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RCBACEF.Models;

namespace RCBACEF.EntityTypeConfigurations
{
    public class ImmutableEntityConfiguration<T>
        : CreatableEntityConfiguration<T>
        where T : ImmutableEntity
    {
        public override void Configure(EntityTypeBuilder<T> entity)
        {
            base.Configure(entity);

            entity.HasOne(u => u.DeletedBy)
                  .WithMany()
                  .HasForeignKey(u => u.DeletedById)
                  .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
