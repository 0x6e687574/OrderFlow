using OrderFlow.Payment.Application.Abstractions.Repositories;

namespace OrderFlow.Payment.Application.Abstractions.UnitOfWorks;

public interface IUnitOfWork
{
    public IPaymentRepository Payments { get; }
    public IOutboxMessageRepository OutboxMessages { get; }

    public Task SaveChangesAsync();
}