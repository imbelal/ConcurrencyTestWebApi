using ConcurrencyTestWebApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConcurrencyTestWebApi.EntityConfigurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasColumnName("Name").HasColumnType("nvarchar(200)").IsRequired();
            builder.Property(x => x.StockQuantity).HasColumnName("StockQuantity").HasColumnType("int").IsRequired();
            builder.Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
