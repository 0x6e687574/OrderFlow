using OrderFlow.Payment.Application.Abstractions.Repositories;
using OrderFlow.Payment.Application.Abstractions.UnitOfWorks;

namespace OrderFlow.Payment.Infrastructure.Persistence.UnitOfWorks;

public class UnitOfWork(
    PaymentDbContext paymentDbContext,
    IPaymentRepository paymentRepository,
    IOutboxMessageRepository outboxMessageRepository)
    : IUnitOfWork
{
    public IPaymentRepository Payments => paymentRepository;
    public IOutboxMessageRepository OutboxMessages => outboxMessageRepository;

    public async Task SaveChangesAsync()
        => await paymentDbContext.SaveChangesAsync();
}