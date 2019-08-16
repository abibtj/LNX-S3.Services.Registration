using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace S3.Services.Registration.Domain.EntityConfigurations
{
    internal class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Line1).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Line2).HasMaxLength(100);
            builder.Property(x => x.Town).HasMaxLength(30).IsRequired();
            builder.Property(x => x.State).HasMaxLength(20).IsRequired();
            builder.Property(x => x.Country).HasMaxLength(20).IsRequired();
        }
    }
}
