using OrderFlow.Inventory.Domain.Entities;

namespace OrderFlow.Inventory.Application.Abstractions.Repositories;

public interface IOutboxMessageRepository
{
    public Task AddAsync(OutboxMessage outboxMessage);
}