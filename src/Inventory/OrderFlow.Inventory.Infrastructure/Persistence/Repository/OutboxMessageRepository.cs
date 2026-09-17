using OrderFlow.Inventory.Application.Abstractions.Repositories;
using OrderFlow.Inventory.Domain.Entities;

namespace OrderFlow.Inventory.Infrastructure.Persistence.Repository;

public class OutboxMessageRepository(InventoryDbContext inventoryDbContext) : IOutboxMessageRepository
{
    public async Task AddAsync(OutboxMessage outboxMessage)
        => await inventoryDbContext.OutboxMessages.AddAsync(outboxMessage);
}