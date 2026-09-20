using Microsoft.EntityFrameworkCore;
using OrderFlow.Payment.Application.Abstractions.Repositories;

namespace OrderFlow.Payment.Infrastructure.Persistence.Repository;

using Payment = Domain.Entities.Payment;

public class PaymentRepository(PaymentDbContext paymentDbContext) : IPaymentRepository
{
    public async Task<Payment?> GetByOrderIdAsync(Guid orderId)
        => await paymentDbContext.Payments
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.OrderId == orderId);
}