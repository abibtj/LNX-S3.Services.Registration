using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace S3.Services.Registration.Domain.EntityConfigurations
{
    internal class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.FirstName).HasMaxLength(30).IsRequired();
            builder.Property(x => x.MiddleName).HasMaxLength(30);
            builder.Property(x => x.LastName).HasMaxLength(30).IsRequired();
            builder.Property(x => x.Gender).HasMaxLength(6).IsRequired();
            builder.Property(x => x.Position).HasMaxLength(50);
            builder.Property(x => x.Email).HasMaxLength(50);
            builder.Property(x => x.PhoneNumber).HasMaxLength(20);

            // Relationships
            builder.HasMany(x => x.ScoresEntryTasks).WithOne(y => y.Teacher).OnDelete(DeleteBehavior.Cascade);
            //builder.HasOne(x => x.Address).WithOne(y => y.Teacher).HasForeignKey<TeacherAddress>(z => z.TeacherId).OnDelete(DeleteBehavior.NoAction); // ToDo: DeleteBehavior.Cascade throwing exception, so delete manually
            builder.HasOne(x => x.Address).WithOne().HasForeignKey<TeacherAddress>(z => z.TeacherId).OnDelete(DeleteBehavior.NoAction); // ToDo: DeleteBehavior.Cascade throwing exception, so delete manually
        }
    }
}
