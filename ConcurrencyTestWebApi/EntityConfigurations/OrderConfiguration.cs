using ConcurrencyTestWebApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConcurrencyTestWebApi.EntityConfigurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ProductId).HasColumnName("ProductId").HasColumnType("uniqueidentifier").IsRequired();
            builder.Property(x => x.OrderQuantity).HasColumnName("OrderQuantity").HasColumnType("int").IsRequired();
        }
    }
}
