using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RCBACEF.Models;

namespace RCBACEF.EntityTypeConfigurations
{
    public class BaseConfiguration<T>
        : IEntityTypeConfiguration<T>
        where T : Base
    {
        public virtual void Configure(EntityTypeBuilder<T> entity)
        {
        }
    }
}
