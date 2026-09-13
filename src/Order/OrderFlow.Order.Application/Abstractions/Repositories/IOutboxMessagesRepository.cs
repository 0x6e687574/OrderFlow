using OrderFlow.Order.Domain.Entities;

namespace OrderFlow.Order.Application.Abstractions.Repositories;

public interface IOutboxMessagesRepository
{
    public Task AddAsync(OutboxMessage outboxMessage);
}