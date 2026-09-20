using OrderFlow.Payment.Domain.Entities;

namespace OrderFlow.Payment.Application.Abstractions.Repositories;

public interface IOutboxMessageRepository
{
    public Task AddAsync(OutboxMessage outboxMessage);
}