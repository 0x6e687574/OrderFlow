using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderFlow.Order.Domain.Entities;

namespace OrderFlow.Order.Infrastructure.Persistence.Configurations;

using Order = Domain.Entities.Order;

public class OrderLineConfiguration : IEntityTypeConfiguration<OrderLine>
{
    public void Configure(EntityTypeBuilder<OrderLine> builder)
    {
        builder.ToTable("order_lines", "orderflow_orders");
        
        builder.HasKey(e => e.Id);

        builder.HasOne<Order>()
            .WithMany(e => e.OrderLines)
            .HasForeignKey(e => e.OrderId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Property(e => e.Sku)
            .HasMaxLength(50)
            .IsRequired();
    }
}