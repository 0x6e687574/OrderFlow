using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderFlow.Payment.Domain.Entities;

namespace OrderFlow.Payment.Infrastructure.Persistence.Configurations;

public class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.ToTable("outbox_messages", "orderflow_payments");

        builder.HasKey(e => e.Id);

        builder.HasIndex(e => e.EventId)
            .IsUnique();

        builder.Property(e => e.Topic)
            .HasMaxLength(255)
            .IsRequired();
    }
}