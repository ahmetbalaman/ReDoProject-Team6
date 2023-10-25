using Microsoft.EntityFrameworkCore;
using ReDoProject.Domain.Entities;
using ReDoProject.Persistence;

namespace ReDoProject.Persistence.Contexts
{
    public class ReDoMusicDbContext : DbContext
    {
        public DbSet<Instrument> Instruments { get; set; }
        public DbSet<Brand> Brands { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // optionsBuilder.UseNpgsql(Configurations.GetString("ConnectionStrings:PostgreSQL
            optionsBuilder.UseNpgsql("Server=91.151.83.102;Port=5432;Database=Team6ReDoProject;User Id=yunusemresenteam;Password=3*ZM44j3bgIBULDrlsyjKB595;");
        }
    }
}
