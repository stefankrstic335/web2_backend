using Backend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;

namespace Backend.Database
{
    public class LocalDbContext : DbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }


        public LocalDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Entity<Order>().HasMany(x => x.OrderedProducts).WithMany(x => x.Orders);
           
        }
    }
}
