using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderFlow.Inventory.Domain.Entities;

namespace OrderFlow.Inventory.Infrastructure.Persistence.Configurations;

public class InboxMessageConfiguration : IEntityTypeConfiguration<InboxMessage>
{
    public void Configure(EntityTypeBuilder<InboxMessage> builder)
    {
        builder.ToTable("inbox_messages", "orderflow_inventory");
        
        builder.HasKey(e => e.EventId);
    }
}