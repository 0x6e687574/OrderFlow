namespace OrderFlow.Payment.Application.Abstractions.Repositories;

using Payment = Domain.Entities.Payment;

public interface IPaymentRepository
{
    public Task<Payment?> GetByOrderIdAsync(Guid orderId);
}