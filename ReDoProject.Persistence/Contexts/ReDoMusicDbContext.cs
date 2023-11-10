using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ReDoProject.Domain.Entities;
using ReDoProject.Persistence;

namespace ReDoProject.Persistence.Contexts
{
    public class ReDoMusicDbContext : DbContext
    {
        public DbSet<Instrument> Instruments { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Basket> Baskets{ get; set; }
        public DbSet<MyLogger> Logs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Your Database Connection String");
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}
