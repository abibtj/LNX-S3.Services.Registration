using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace S3.Services.Registration.Domain.EntityConfigurations
{
    internal class ScoresEntryTaskConfiguration : IEntityTypeConfiguration<ScoresEntryTask>
    {
        public void Configure(EntityTypeBuilder<ScoresEntryTask> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.TeacherId).IsRequired();
            builder.Property(x => x.TeacherName).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Subject).IsRequired();
            builder.Property(x => x.ClassId).IsRequired();
            builder.Property(x => x.ClassName).HasMaxLength(20).IsRequired();
        }
    }
}
