using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderFlow.Inventory.Domain.Entities;

namespace OrderFlow.Inventory.Infrastructure.Persistence.Configurations;

public class StockItemConfiguration : IEntityTypeConfiguration<StockItem>
{
    public void Configure(EntityTypeBuilder<StockItem> builder)
    {
        builder.ToTable("stock_items", "orderflow_inventory");

        builder.HasKey(e => e.Sku);

        builder.Property(e => e.Sku)
            .HasMaxLength(50)
            .IsRequired();
    }
}