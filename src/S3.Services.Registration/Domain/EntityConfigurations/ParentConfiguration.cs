using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace S3.Services.Registration.Domain.EntityConfigurations
{
    internal class ParentConfiguration : IEntityTypeConfiguration<Parent>
    {
        public void Configure(EntityTypeBuilder<Parent> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.RegNumber).HasMaxLength(12).IsRequired();
            builder.HasIndex(x => x.RegNumber).IsUnique(); // Make RegNumber unique
            builder.Property(x => x.FirstName).HasMaxLength(30).IsRequired();
            builder.Property(x => x.MiddleName).HasMaxLength(30);
            builder.Property(x => x.LastName).HasMaxLength(30).IsRequired();
            builder.Property(x => x.Gender).HasMaxLength(6).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(50);
            builder.Property(x => x.PhoneNumber).HasMaxLength(20);

            // Relationships
            builder.HasMany(x => x.Students).WithOne(y => y.Parent).OnDelete(DeleteBehavior.ClientSetNull);
            builder.HasOne(x => x.Address).WithOne(y => y.Parent).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
