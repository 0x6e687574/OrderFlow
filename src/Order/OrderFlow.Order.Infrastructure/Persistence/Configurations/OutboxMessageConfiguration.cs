using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderFlow.Order.Domain.Entities;

namespace OrderFlow.Order.Infrastructure.Persistence.Configurations;

public class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.ToTable("outbox_messages", "orderflow_orders");
        
        builder.HasKey(e => e.Id);

        builder.HasIndex(e => e.EventId)
            .IsUnique();

        builder.Property(e => e.Topic)
            .HasMaxLength(255)
            .IsRequired();
    }
}