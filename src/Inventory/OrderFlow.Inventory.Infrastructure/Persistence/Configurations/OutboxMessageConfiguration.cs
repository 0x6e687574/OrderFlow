using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderFlow.Inventory.Domain.Entities;

namespace OrderFlow.Inventory.Infrastructure.Persistence.Configurations;

public class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.ToTable("outbox_messages", "orderflow_inventory");

        builder.HasKey(e => e.Id);

        builder.HasIndex(e => e.EventId)
            .IsUnique();

        builder.Property(e => e.Topic)
            .HasMaxLength(255)
            .IsRequired();
    }
}