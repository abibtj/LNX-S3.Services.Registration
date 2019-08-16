using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace S3.Services.Registration.Domain.EntityConfigurations
{
    internal class SchoolConfiguration : IEntityTypeConfiguration<School>
    {
        public void Configure(EntityTypeBuilder<School> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Category).HasMaxLength(30).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
            builder.HasIndex(x => x.Name).IsUnique(); // Make name unique

            // Relationships
            builder.HasMany(x => x.Classes).WithOne(y => y.School).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.Students).WithOne(y => y.School).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.Teachers).WithOne(y => y.School).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
