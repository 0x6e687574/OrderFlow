using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderFlow.Order.Domain.Entities;

namespace OrderFlow.Order.Infrastructure.Persistence.Configurations;

using Order = Domain.Entities.Order;

public class OrderSagaStateConfiguration : IEntityTypeConfiguration<OrderSagaState>
{
    public void Configure(EntityTypeBuilder<OrderSagaState> builder)
    {
        builder.ToTable("order_saga_state", "orderflow_orders");
        
        builder.HasKey(e => e.OrderId);

        builder.HasOne<Order>()
            .WithOne()
            .HasForeignKey<OrderSagaState>(e => e.OrderId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}