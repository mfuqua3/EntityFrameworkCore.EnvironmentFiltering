using EnvironmentSeedingExample.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EnvironmentSeedingExample.Data;

public class MyAppDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
    public MyAppDbContext(DbContextOptions<MyAppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var assembly = GetType().Assembly;
        modelBuilder.ApplyConfigurationsFromAssembly(assembly);
        base.OnModelCreating(modelBuilder);
    }
}