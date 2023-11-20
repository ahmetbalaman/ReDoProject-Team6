using System.Reflection;
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
        
        public ReDoMusicDbContext(DbContextOptions<ReDoMusicDbContext> options) : base(options)
        {
            
        }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); 
            base.OnModelCreating(modelBuilder);
        }
     

    }
}
