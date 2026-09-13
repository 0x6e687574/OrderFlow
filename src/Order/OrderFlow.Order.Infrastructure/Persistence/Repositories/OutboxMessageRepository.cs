using OrderFlow.Order.Application.Abstractions.Repositories;
using OrderFlow.Order.Domain.Entities;

namespace OrderFlow.Order.Infrastructure.Persistence.Repositories;

public class OutboxMessageRepository(OrderDbContext orderDbContext) : IOutboxMessagesRepository
{
    public async Task AddAsync(OutboxMessage outboxMessage)
        => await orderDbContext.OutboxMessages.AddAsync(outboxMessage);
}