using OrderFlow.Payment.Application.Abstractions.Repositories;
using OrderFlow.Payment.Domain.Entities;

namespace OrderFlow.Payment.Infrastructure.Persistence.Repository;

public class OutboxMessageRepository(PaymentDbContext paymentDbContext) : IOutboxMessageRepository
{
    public async Task AddAsync(OutboxMessage outboxMessage)
        => await paymentDbContext.OutboxMessages.AddAsync(outboxMessage);
}