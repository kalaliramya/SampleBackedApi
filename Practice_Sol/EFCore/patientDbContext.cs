using Microsoft.EntityFrameworkCore;

namespace Samplebacked_api.EFCore
{
    public class patientDbContext : DbContext
    {
        public patientDbContext(DbContextOptions<patientDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();
        }


        public DbSet<Patient> patients { get; set; }
        
   

    }
}
