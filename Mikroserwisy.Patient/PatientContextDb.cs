using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

namespace Mikroserwisy.PatientApi
{
    public class PatientContextDb : DbContext
    {
        private readonly IConfiguration _configuration;
        public DbSet<Entities.Patient> Patients { get; set; }
        public PatientContextDb(IConfiguration configuration)
            : base()
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"server=(localdb)\MSSQLLocalDB;database=patient-dev;trusted_connection=true;",
                options => options.MigrationsHistoryTable("__EFMigrationsHistory", "ProjectSzT3")
            );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
