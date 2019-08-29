using Microsoft.EntityFrameworkCore;
using S3.Services.Registration.Domain;
using S3.Services.Registration.Domain.EntityConfigurations;

namespace S3.Services.Registration.Utility
{
    public class RegistrationDbContext : DbContext
    {
        public RegistrationDbContext(DbContextOptions<RegistrationDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<Address> Address { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Parent> Parents { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<ScoresEntryTask> ScoresEntryTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Apply custum configurations using reflextion to scan for configuration files
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Startup).Assembly);
        }
    }
}
