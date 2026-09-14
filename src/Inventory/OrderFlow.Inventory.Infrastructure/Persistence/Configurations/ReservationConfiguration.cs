using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderFlow.Inventory.Domain.Entities;

namespace OrderFlow.Inventory.Infrastructure.Persistence.Configurations;

public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.ToTable("reservations", "orderflow_inventory");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Sku)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasIndex(e => new
            {
                e.OrderId,
                e.Sku
            })
            .IsUnique();
    }
}