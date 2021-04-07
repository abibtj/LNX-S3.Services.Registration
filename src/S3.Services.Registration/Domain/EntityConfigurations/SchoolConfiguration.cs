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
            builder.Property(x => x.Email).HasMaxLength(30).IsRequired();
            builder.Property(x => x.PhoneNumber).HasMaxLength(50).IsRequired();

            // Relationships
            builder.HasOne(x => x.Address).WithOne().HasForeignKey<SchoolAddress>(z => z.SchoolId).OnDelete(DeleteBehavior.Cascade);
            //builder.HasOne(x => x.Address).WithOne(y => y.School).HasForeignKey<SchoolAddress>(z => z.SchoolId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.Classes).WithOne(y => y.School).OnDelete(DeleteBehavior.Restrict); // Delete manually because multiple cascade path is not allowed (Class' Students)
            builder.HasMany(x => x.Students).WithOne(y => y.School).OnDelete(DeleteBehavior.Restrict); // Delete manually because multiple cascade path is not allowed (Student's Address)
            builder.HasMany(x => x.Teachers).WithOne(y => y.School).OnDelete(DeleteBehavior.Restrict); // Delete manually because multiple cascade path is not allowed (Teacher's Address)
        }
    }
}
