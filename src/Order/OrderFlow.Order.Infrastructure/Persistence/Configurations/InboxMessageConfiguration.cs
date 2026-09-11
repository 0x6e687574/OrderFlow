using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderFlow.Order.Domain.Entities;

namespace OrderFlow.Order.Infrastructure.Persistence.Configurations;

public class InboxMessageConfiguration : IEntityTypeConfiguration<InboxMessage>
{
    public void Configure(EntityTypeBuilder<InboxMessage> builder)
    {
        builder.ToTable("inbox_messages", "orderflow_orders");

        builder.HasKey(e => e.EventId);
    }
}