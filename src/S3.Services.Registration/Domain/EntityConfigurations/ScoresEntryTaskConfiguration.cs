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
            builder.Property(x => x.SubjectId).IsRequired();
            builder.Property(x => x.ClassId).IsRequired();
        }
    }
}
