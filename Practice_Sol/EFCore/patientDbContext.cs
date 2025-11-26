using Microsoft.EntityFrameworkCore;
using Samplebacked_api.EFCore.PatientEF;
using Samplebacked_api.EFCore.UserEF;

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
        public DbSet<User> users { get; set; }
        public DbSet<Roles> roles { get; set; }



    }
}
