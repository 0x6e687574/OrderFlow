using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OrderFlow.Order.Infrastructure.Persistence.Configurations;

using Order = Domain.Entities.Order;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("orders", "orderflow_orders");
        
        builder.HasKey(e => e.Id);
        
        builder.Property(e => e.CustomerId)
            .HasMaxLength(100)
            .IsRequired();
    }
}