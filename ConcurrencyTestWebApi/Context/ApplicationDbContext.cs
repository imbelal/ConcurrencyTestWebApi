using ConcurrencyTestWebApi.Entities;
using ConcurrencyTestWebApi.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace ConcurrencyTestWebApi.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Add all external entity configurations
            modelBuilder.ApplyConfiguration<Product>(new ProductConfiguration());
            modelBuilder.ApplyConfiguration<Order>(new OrderConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
