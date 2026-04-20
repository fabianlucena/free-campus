using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RCBACEF.Models;

namespace RCBACEF.EntityTypeConfigurations
{
    public class CompanyConfiguration : SoftDeletableEntityConfiguration<Company>
    {
        public override void Configure(EntityTypeBuilder<Company> entity)
        {
            base.Configure(entity);
        }
    }
}
