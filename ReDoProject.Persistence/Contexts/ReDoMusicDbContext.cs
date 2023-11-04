﻿using Microsoft.EntityFrameworkCore;
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
            // optionsBuilder.UseNpgsql(Configurations.GetString("ConnectionStrings:PostgreSQL
            //            optionsBuilder.UseNpgsql("Server=91.151.83.102;Port=5432;Database=!!!06TeamReDoProject;User Id=yunusemresenteam;Password=3*ZM44j3bgIBULDrlsyjKB595;");
            optionsBuilder.UseNpgsql("Your Connetion Link");
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}
