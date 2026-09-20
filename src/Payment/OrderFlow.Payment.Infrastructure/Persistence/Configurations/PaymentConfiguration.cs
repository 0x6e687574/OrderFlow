using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OrderFlow.Payment.Infrastructure.Persistence.Configurations;

using Payment = Domain.Entities.Payment;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("payments", "orderflow_payments");

        builder.HasKey(e => e.Id);

        builder.HasIndex(e => e.OrderId)
            .IsUnique();
    }
}