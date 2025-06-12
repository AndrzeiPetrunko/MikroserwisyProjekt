using Microsoft.EntityFrameworkCore;

namespace Mikroserwisy.Doctor
{
    public class DoctorContextDb : DbContext
    {
        private readonly IConfiguration _configuration;
        public DbSet<Entities.Doctor> Doctors { get; set; }
        public DoctorContextDb(IConfiguration configuration)
            : base()
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"server=(localdb)\MSSQLLocalDB;database=doctor-dev;trusted_connection=true;",
                options => options.MigrationsHistoryTable("__EFMigrationsHistory", "ProjectSzT3")
            );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

